using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Core;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Core.Logging;

namespace Vokseverk {
	[PropertyValueType(typeof(VersionNumber))]
	[PropertyValueCache(PropertyCacheValue.All, PropertyCacheLevel.Content)]
	public class VersionNumberPropertyConverter : PropertyValueConverterBase {
		
		public override bool IsConverter(PublishedPropertyType propertyType) {
			return propertyType.PropertyEditorAlias.Equals("Vokseverk.VersionNumber");
		}
		
		public override object ConvertDataToSource(PublishedPropertyType propertyType, object data, bool preview) {
			if (data == null) {
				return data;
			}
			
			LogHelper.Info(this.GetType(), "Raw data: " + data.ToString());
			var version = new VersionNumber(1, 0, 0);
			
			var source = data.ToString();
			if (source.DetectIsJson()) {
				try {
					var json = JsonConvert.DeserializeObject<JObject>(source);
					version = new VersionNumber(json["major"], json["minor"], json["patch"]);
				}
				catch { /* Hmm, not JSON after all ... */ }
			}
			
			return version;
		}
		
		public class VersionNumber {
			public VersionNumber(int major, int minor, int patch) {
				Major = major;
				Minor = minor;
				Patch = patch;
			}
			
			public VersionNumber(object major, object minor, object patch) {
				Major = 1;
				Minor = -1;
				Patch = -1;
				
				int maj, min, pat;
				if (Int32.TryParse(major.ToString(), out maj)) {
					Major = maj;
				}
				if (Int32.TryParse(minor.ToString(), out min)) {
					Minor = min;
				}
				if (Int32.TryParse(patch.ToString(), out pat)) {
					Patch = pat;
				}
			}
			
			public int Major { get; }
			public int Minor { get; }
			public int Patch { get; }
			
			public override string ToString() {
				string versionFormat = "{0}.{1}.{2}";
				if (Patch == -1) {
					versionFormat = "{0}.{1}";
					return string.Format(versionFormat, Major, Minor);
				}
				if (Minor == -1) {
					versionFormat = "{0}";
					return string.Format(versionFormat, Major);
				}
				
				return string.Format(versionFormat, Major, Minor, Patch);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Core;
using Umbraco.Core.Models;
using Umbraco.Core.PropertyEditors;
using Umbraco.Core.Models.PublishedContent;

namespace Vokseverk {
	
	public class VersionNumberPropertyConverter : IPropertyValueConverter {
		
		public bool IsConverter(IPublishedPropertyType propertyType) {
			return propertyType.EditorAlias.Equals("Vokseverk.VersionNumber");
		}
		
		public Type GetPropertyValueType(IPublishedPropertyType propertyType) {
			return typeof(VersionNumber);
		}
		
		public PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) {
			return PropertyCacheLevel.Element;
		}
		
		public bool? IsValue(object value, PropertyValueLevel level) {
			switch (level) {
				case PropertyValueLevel.Source:
				return value != null && value is VersionNumber;
				default:
					throw new NotSupportedException($"Invalid level: {level}.");
			}
		}
		
		public object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) {
			if (source == null) {
				return source;
			}
			
			bool usePatch = UsePatch(propertyType);
			var inter = new VersionNumber(1, 0, usePatch ? 0 : -1);
			
			var ssource = source.ToString();
			if (ssource.DetectIsJson()) {
				try {
					var json = JsonConvert.DeserializeObject<JObject>(ssource);
					inter = new VersionNumber(json["major"], json["minor"], usePatch ? json["patch"] : -1);
				}
				catch { /* Hmm, not JSON after all ... */ }
			}
			
			return inter;
		}
		
		public object ConvertIntermediateToObject(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
			return inter;
		}
		
		public object ConvertIntermediateToXPath(IPublishedElement owner, IPublishedPropertyType propertyType, PropertyCacheLevel referenceCacheLevel, object inter, bool preview) {
			if (inter == null) {
				return null;
			}
			
			// TODO: Implement?
			
			return inter.ToString();
		}
		
		private bool UsePatch(IPublishedPropertyType propertyType) {
			var config = ConfigurationEditor.ConfigurationAs<Dictionary<string, object>>(propertyType.DataType.Configuration);
			return config["usePatch"].ToString() == "1";
		}
		
		public class VersionNumber {
			public VersionNumber(int major, int minor, int patch) {
				Major = major;
				Minor = minor;
				Patch = patch;
			}
			
			public VersionNumber(object major, object minor, object patch) {
				Major = 1;
				Minor = 0;
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
				
				return string.Format(versionFormat, Major, Minor, Patch);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Umbraco.Extensions;
using Umbraco.Cms.Core;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.PropertyEditors;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace Vokseverk {

	public class VersionNumberPropertyConverter : IPropertyValueConverter {

		public bool IsConverter(IPublishedPropertyType propertyType) {
			return propertyType.EditorAlias.Equals("Vokseverk.VersionNumberEditor");
		}

		public Type GetPropertyValueType(IPublishedPropertyType propertyType) {
			return typeof(Version);
		}

		public PropertyCacheLevel GetPropertyCacheLevel(IPublishedPropertyType propertyType) {
			return PropertyCacheLevel.Element;
		}

		public bool? IsValue(object value, PropertyValueLevel level) {
			switch (level) {
				case PropertyValueLevel.Source:
				return value != null && value is Version;
				default:
					throw new NotSupportedException($"Invalid level: {level}.");
			}
		}

		public object ConvertSourceToIntermediate(IPublishedElement owner, IPublishedPropertyType propertyType, object source, bool preview) {
			if (source == null) {
				return source;
			}

			bool usePatch = UsePatch(propertyType);
			var inter = usePatch ? new Version(1, 0, 0) : new Version(1, 0);

			var ssource = source.ToString();
			if (ssource.DetectIsJson()) {
				try {
					var json = JsonConvert.DeserializeObject<JObject>(ssource);
					inter = usePatch
						? new Version((int)json["major"], (int)json["minor"], (int)json["build"])
						: new Version((int)json["major"], (int)json["minor"]);
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

	}
}

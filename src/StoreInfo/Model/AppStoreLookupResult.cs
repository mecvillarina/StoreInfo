using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Plugin.StoreInfo
{
    internal class AppStoreLookupRoot
    {
        [JsonPropertyName("resultCount")]
        public long ResultCount { get; set; }

        [JsonPropertyName("results")]
        public List<AppStoreLookupResult> LookupResults { get; set; }
    }

    internal class AppStoreLookupResult
    {
        [JsonPropertyName("currentVersionReleaseDate")]
        public DateTimeOffset CurrentVersionReleaseDate { get; set; }

        [JsonPropertyName("version")]
        public string Version { get; set; }

        [JsonPropertyName("trackViewUrl")]
        public Uri TrackViewUrl { get; set; }
    }
}

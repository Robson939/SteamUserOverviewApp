using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    class Stat
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("defaultvalue")] public int DefaultValue { get; set; }
        [JsonPropertyName("displayName")] public string DisplayName { get; set; } 
    }
}

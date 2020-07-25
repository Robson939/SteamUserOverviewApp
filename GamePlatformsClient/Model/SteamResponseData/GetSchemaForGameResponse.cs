using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Schema;

namespace GamePlatformsClient.SteamResponseData
{
    class GetSchemaForGameResponse
    {
        [JsonPropertyName("gameName")] public string GameName { get; set; }
        [JsonPropertyName("gameVersion")] public string GameVersion { get; set; }
        [JsonPropertyName("availableGameStats")] public AvailableGameStats AvailableGameStats { get; set; }
    }
}

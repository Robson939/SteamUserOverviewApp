using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    class AvailableGameStats
    {
        [JsonPropertyName("achievements")] public Achievement[] Achievements { get; set; }
        [JsonPropertyName("stats")] public Stat[] Stats { get; set; }
    }
}

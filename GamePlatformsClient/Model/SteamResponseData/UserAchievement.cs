using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    class UserAchievement
    {
        [JsonPropertyName("apiname")] public string Apiname { get; set; }
        [JsonPropertyName("achieved")] public int Achieved { get; set; }
        [JsonPropertyName("unlocktime")] public long UnlockTime { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
    }
}
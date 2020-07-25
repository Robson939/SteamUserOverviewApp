using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    class GetPlayerAchievementsResponse
    {
        [JsonPropertyName("steamid")] public string SteamId { get; set; }

        [JsonPropertyName("gameName")] public string GameName { get; set; }

        [JsonPropertyName("achievements")] public UserAchievement[] Achievements { get; set; }
    }
}

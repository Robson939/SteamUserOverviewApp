using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    class GetPlayerAchievementsResult
    {
        [JsonPropertyName("playerstats")] public GetPlayerAchievementsResponse PlayerStats { get; set; }
    }
}

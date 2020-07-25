using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class GetPlayerAchievementsData
    {
        public class Rootobject
        {
            [JsonPropertyName("playerstats")] public Playerstats Playerstats { get; set; }
        }

        public class Playerstats
        {
            [JsonPropertyName("steamID")] public string SteamID { get; set; }
            [JsonPropertyName("gameName")] public string GameName { get; set; }
            [JsonPropertyName("achievements")] public Achievement[] Achievements { get; set; }
            [JsonPropertyName("success")] public bool Success { get; set; }
        }

        public class Achievement
        {
            [JsonPropertyName("apiname")] public string Apiname { get; set; }
            [JsonPropertyName("achieved")] public int Achieved { get; set; }
            [JsonPropertyName("unlocktime")] public int Unlocktime { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("description")] public string Description { get; set; }
        }

    }
}

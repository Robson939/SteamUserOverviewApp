using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class GetUserStatsForGameData
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
            [JsonPropertyName("stats")] public Stat[] Stats { get; set; }
        }

        public class Achievement
        {
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("achieved")] public int Achieved { get; set; }
        }

        public class Stat
        {
            [JsonPropertyName("appnamelist")] public string Name { get; set; }
            [JsonPropertyName("value")] public int Value { get; set; }
        }

    }
}

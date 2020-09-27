using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.Model.SteamResponseData
{
    public class GetSchemaForGameData
    {
        public class Rootobject
        {
            [JsonPropertyName("game")] public Game Game { get; set; }
        }

        public class Game
        {
            [JsonPropertyName("gameName")] public string GameName { get; set; }
            [JsonPropertyName("gameVersion")] public string GameVersion { get; set; }
            [JsonPropertyName("availableGameStats")] public Availablegamestats AvailableGameStats { get; set; }
        }

        public class Availablegamestats
        {
            [JsonPropertyName("achievements")] public Achievement[] Achievements { get; set; }
            [JsonPropertyName("stats")] public Stat[] Stats { get; set; }
        }

        public class Achievement
        {
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("defaultvalue")] public Int64 Defaultvalue { get; set; }
            [JsonPropertyName("displayName")] public string DisplayName { get; set; }
            [JsonPropertyName("hidden")] public int Hidden { get; set; }
            [JsonPropertyName("description")] public string Description { get; set; }
            [JsonPropertyName("icon")] public string Icon { get; set; }
            [JsonPropertyName("icongray")] public string Icongray { get; set; }
        }

        public class Stat
        {
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("defaultvalue")] public int Defaultvalue { get; set; }
            [JsonPropertyName("displayName")] public string DisplayName { get; set; }
        }

    }
}

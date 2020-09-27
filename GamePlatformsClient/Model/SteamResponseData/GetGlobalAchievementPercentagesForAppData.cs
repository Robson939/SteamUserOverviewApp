using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.Model.SteamResponseData
{
    public class GetGlobalAchievementPercentagesForAppData
    {
        public class Rootobject
        {
            [JsonPropertyName("achievementpercentages")] public Achievementpercentages Achievementpercentages { get; set; }
        }

        public class Achievementpercentages
        {
            [JsonPropertyName("achievements")] public Achievement[] Achievements { get; set; }
        }

        public class Achievement
        {
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("percent")] public float Percent { get; set; }
        }

    }
}

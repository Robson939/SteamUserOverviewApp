using System;
using System.Collections.Generic;
using System.Security.RightsManagement;
using System.Text;
using System.Text.Json.Serialization;
using System.Windows.Controls;

namespace GamePlatformsClient.SteamResponseData
{
    class Achievement
    {
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("defaultvalue")] public int DefaultValue { get; set; }
        [JsonPropertyName("displayName")] public string DisplayName { get; set; }
        [JsonPropertyName("hidden")] public int Hidden { get; set; }
        [JsonPropertyName("description")] public string Description { get; set; }
        [JsonPropertyName("icon")] public string Icon { get; set; }
        [JsonPropertyName("icongray")] public string IconGray { get; set; }

        public Image iconImg;
        public Image iconGrayImg;
    }
}

//{"name":"ACHIEVEMENT_WIN_WASHINGTON","defaultvalue":0,"displayName":"First in the Hearts of Your Countrymen","hidden":0,"description":"Beat the game on any difficulty setting as Washington.","icon":"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/8930/4cf17a59d70b2decfd4369de8a7429e7b00f5ab8.jpg","icongray":"https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/8930/2ce109f9be6cb3193a385444b9b0b0ffcc7b2219.jpg"}
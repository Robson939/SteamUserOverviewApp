using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class App
    {
        [JsonPropertyName("appid")] public UInt32 Appid { get; set; }
        [JsonPropertyName("name")] public string Name { get; set; }
        [JsonPropertyName("img_icon_url")] public string IconUrl { get; set; }
    }
}

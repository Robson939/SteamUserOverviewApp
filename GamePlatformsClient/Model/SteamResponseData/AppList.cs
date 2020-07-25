using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class AppList
    {
        [JsonPropertyName("apps")] public App[] Apps { get; set; }
    }
}

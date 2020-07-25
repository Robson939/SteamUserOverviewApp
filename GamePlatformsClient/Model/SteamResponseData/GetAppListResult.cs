using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class GetAppListResult
    {
        [JsonPropertyName("applist")] public AppList Applist { get; set; }
    }
}

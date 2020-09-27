using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.Model.SteamResponseData
{
    public class GetAppListData
    {
        public class Rootobject
        {
            [JsonPropertyName("applist")] public Applist Applist { get; set; }
        }

        public class Applist
        {
            [JsonPropertyName("apps")] public App[] Apps { get; set; }
        }

        public class App
        {
            [JsonPropertyName("appid")] public int Appid { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; }
        }

    }
}

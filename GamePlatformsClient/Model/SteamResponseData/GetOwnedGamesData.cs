using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.Model.SteamResponseData
{
    public class GetOwnedGamesData
    {

        public class Rootobject
        {
            [JsonPropertyName("response")] public Response Response { get; set; }
        }

        public class Response
        {
            [JsonPropertyName("game_count")] public int Game_count { get; set; }
            [JsonPropertyName("games")] public Game[] Games { get; set; }
        }

        public class Game
        {
            [JsonPropertyName("appid")] public uint Appid { get; set; }
            [JsonPropertyName("name")] public string Name { get; set; }
            [JsonPropertyName("playtime_forever")] public int Playtime_forever { get; set; }
            [JsonPropertyName("img_icon_url")] public string Img_icon_url { get; set; }
            [JsonPropertyName("img_logo_url")] public string Img_logo_url { get; set; }
            [JsonPropertyName("has_community_visible_stats")] public bool Has_community_visible_stats { get; set; }
            [JsonPropertyName("playtime_windows_forever")] public int Playtime_windows_forever { get; set; }
            [JsonPropertyName("playtime_mac_forever")] public int Playtime_mac_forever { get; set; }
            [JsonPropertyName("playtime_linux_forever")] public int Playtime_linux_forever { get; set; }
            [JsonPropertyName("playtime_2weeks")] public int Playtime_2weeks { get; set; }
        }

    }
}

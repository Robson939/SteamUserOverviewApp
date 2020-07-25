using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class GetOwnedGamesResponse
    {
        [JsonPropertyName("game_count")] public UInt32 GameCount { get; set; }
        [JsonPropertyName("games")] public App[] Games { get; set; }



    }
}

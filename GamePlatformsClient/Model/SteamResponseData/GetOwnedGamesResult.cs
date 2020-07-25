using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class GetOwnedGamesResult
    {
        [JsonPropertyName("response")]
        public GetOwnedGamesResponse Response { get; set; }

    }
}

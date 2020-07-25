using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class ResolveVanityURLResult
    {
        [JsonPropertyName("response")] public ResolveVanityURLResponse Response { get; set; }
    }
}
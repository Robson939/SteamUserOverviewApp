using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class ResolveVanityURLResponse
    {
        [JsonPropertyName("steamid")] public string SteamId { get; set; }
        [JsonPropertyName("success")] public int Success { get; set; }
    }
}

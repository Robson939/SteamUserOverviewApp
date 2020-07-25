using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    class GetSchemaForGameResult
    {
        [JsonPropertyName("game")] public GetSchemaForGameResponse Game { get; set; }
    }
}

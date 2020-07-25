using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace GamePlatformsClient.SteamResponseData
{
    public class ResolveVanityURLData
    {
        public class Rootobject
        {
            [JsonPropertyName("response")] public Response Response { get; set; }
        }

        public class Response
        {
            [JsonPropertyName("steamid")] public string Steamid { get; set; }
            [JsonPropertyName("success")] public int Success { get; set; }
        }

    }
}

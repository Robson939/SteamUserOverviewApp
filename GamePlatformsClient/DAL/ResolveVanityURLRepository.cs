using GamePlatformsClient.SteamResponseData;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace GamePlatformsClient.DAL
{
    public class ResolveVanityURLRepository : IResolveVanityURLRepository
    {
        public async Task<ResolveVanityURLData.Rootobject> Get(string nickname, CancellationTokenSource cancellationTokenSource)
        {
            HttpClient httpClient = HttpClientFactory.Create();

            var response = await httpClient.GetAsync(SteamApiRequestManager.GetResolveVanityURL(nickname), cancellationTokenSource.Token);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResolveVanityURLData.Rootobject>(content);
        }
    }
}
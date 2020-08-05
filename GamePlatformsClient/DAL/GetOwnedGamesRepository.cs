using System;
using GamePlatformsClient.SteamResponseData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;

namespace GamePlatformsClient.DAL
{
    class GetOwnedGamesRepository : IGetOwnedGamesRepository
    {
        public async Task<GetOwnedGamesData.Rootobject> Get(string userSteamId, CancellationTokenSource cancellationTokenSource)
        {
            HttpClient httpClient = HttpClientFactory.Create();

            var response = await httpClient.GetAsync(SteamApiRequestManager.GetOwnedGames(userSteamId), cancellationTokenSource.Token);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetOwnedGamesData.Rootobject>(content);
        }
    }
}

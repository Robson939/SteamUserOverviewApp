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
    class GetPlayerAchievementsRepository : IGetPlayerAchievementsRepository
    {
        public async Task<GetPlayerAchievementsData.Rootobject> Get(string userSteamID, string appid, CancellationTokenSource cancellationTokenSource)
        {
            HttpClient httpClient = HttpClientFactory.Create();

            HttpResponseMessage response = await httpClient.GetAsync(SteamApiRequestManager.GetPlayerAchievements(userSteamID, appid), cancellationTokenSource.Token);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetPlayerAchievementsData.Rootobject>(content);
        }
    }
}

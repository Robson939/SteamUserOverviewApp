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
    public class SteamService : ISteamService
    {
        public async Task<GetAppListData.Rootobject> GetAppListData(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public async Task<GetOwnedGamesData.Rootobject> GetOwnedGamesData(string userSteamId, CancellationToken cancellationToken)
        {
            HttpClient httpClient = HttpClientFactory.Create();

            var response = await httpClient.GetAsync(SteamApiRequestManager.GetOwnedGames(userSteamId), cancellationToken);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetOwnedGamesData.Rootobject>(content);
        }

        public async Task<GetPlayerAchievementsData.Rootobject> GetPlayerAchievementsData(string userSteamID, string appid, CancellationToken cancellationToken)
        {
            HttpClient httpClient = HttpClientFactory.Create();

            HttpResponseMessage response = await httpClient.GetAsync(SteamApiRequestManager.GetPlayerAchievements(userSteamID, appid), cancellationToken);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<GetPlayerAchievementsData.Rootobject>(content);
        }

        public async Task<GetSchemaForGameData.Rootobject> GetSchemaForGameData(string appid, CancellationToken cancellationToken)
        {
            HttpClient httpClient = HttpClientFactory.Create();

            HttpResponseMessage response =
                await httpClient.GetAsync(SteamApiRequestManager.GetSchemaForGame(appid), cancellationToken);
            response.EnsureSuccessStatusCode();
            string content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<GetSchemaForGameData.Rootobject>(content);
        }

        public async Task<ResolveVanityURLData.Rootobject> ResolveVanityURLData(string nickname, CancellationToken cancellationToken)
        {
            HttpClient httpClient = HttpClientFactory.Create();

            var response = await httpClient.GetAsync(SteamApiRequestManager.GetResolveVanityURL(nickname), cancellationToken);
            response.EnsureSuccessStatusCode();

            string content = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<ResolveVanityURLData.Rootobject>(content);
        }
    }
}

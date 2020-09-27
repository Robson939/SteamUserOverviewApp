using GamePlatformsClient.Model.SteamResponseData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamePlatformsClient.DAL
{
    interface ISteamService
    {
        public Task<GetAppListData.Rootobject> GetAppListData(CancellationToken cancellationToken);
        public Task<GetOwnedGamesData.Rootobject> GetOwnedGamesData(string userSteamId, CancellationToken cancellationToken);
        public Task<GetPlayerAchievementsData.Rootobject> GetPlayerAchievementsData(string userSteamID, string appid, CancellationToken cancellationToken);
        public Task<GetUserStatsForGameData.Rootobject> GetUserStatsForGame(string userSteamID, string appid, CancellationToken cancellationToken);
        public Task<GetSchemaForGameData.Rootobject> GetSchemaForGameData(string appid, CancellationToken cancellationToken);
        public Task<ResolveVanityURLData.Rootobject> ResolveVanityURLData(string nickname, CancellationToken cancellationToken);
        public Task<GetGlobalAchievementPercentagesForAppData.Rootobject> GetGlobalAchievementPercentagesForAppData(string gameid, CancellationToken cancellationToken);
    }
}
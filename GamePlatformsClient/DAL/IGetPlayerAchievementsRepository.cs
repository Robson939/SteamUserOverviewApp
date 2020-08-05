using GamePlatformsClient.SteamResponseData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamePlatformsClient.DAL
{
    interface IGetPlayerAchievementsRepository
    {
        public Task<GetPlayerAchievementsData.Rootobject> Get(string userSteamID, string appid, CancellationTokenSource cancellationTokenSource);
    }
}

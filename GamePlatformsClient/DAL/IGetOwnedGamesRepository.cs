using GamePlatformsClient.SteamResponseData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamePlatformsClient.DAL
{
    interface IGetOwnedGamesRepository
    {
        public Task<GetOwnedGamesData.Rootobject> Get(string userSteamId, CancellationTokenSource cancellationTokenSource);
    }
}
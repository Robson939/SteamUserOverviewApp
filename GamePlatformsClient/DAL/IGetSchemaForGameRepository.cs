using GamePlatformsClient.SteamResponseData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamePlatformsClient.DAL
{
    interface IGetSchemaForGameRepository
    {
        public Task<GetSchemaForGameData.Rootobject> Get(string appid, CancellationTokenSource cancellationTokenSource);
    }
}

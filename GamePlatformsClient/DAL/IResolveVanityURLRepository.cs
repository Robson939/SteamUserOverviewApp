using GamePlatformsClient.SteamResponseData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamePlatformsClient.DAL
{
    interface IResolveVanityURLRepository
    {
        public Task<ResolveVanityURLData.Rootobject> Get(string nickname, CancellationTokenSource cancellationTokenSource);
    }
}
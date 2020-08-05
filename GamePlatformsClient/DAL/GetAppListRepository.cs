using GamePlatformsClient.SteamResponseData;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GamePlatformsClient.DAL
{
    class GetAppListRepository : IGetAppListRepository
    {
        public Task<GetAppListData.Rootobject> Get(CancellationTokenSource cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

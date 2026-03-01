using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Services.Contract
{
    public interface ICacheService
    {
        Task SetCacheKeyAsync(string Key,object response,TimeSpan expireTime);
        Task<string> GetCacheKeyAsync(string Key);
    }
}

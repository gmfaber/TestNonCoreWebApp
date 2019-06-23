using System;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestNonCoreWebApp.Helpers
{
    public static class IPCacheManager
    {
        private static MemoryCache _cache = MemoryCache.Default;

        public static HashSet<string> IPCache
        {
            get
            {
                if (_cache == null || !_cache.Contains("IPList"))
                    RefreshIPCache();

                return _cache.Get("IPList") as HashSet<string>;
            }
        }
        

        public static void RefreshIPCache()
        {
            var ipCache = new HashSet<string>();
            ipCache.Add("::1");

            var cachItemPolicy = new CacheItemPolicy();
            cachItemPolicy.AbsoluteExpiration = DateTime.Now.AddMinutes(2);
            _cache.Add("IPList", ipCache, cachItemPolicy);
        }
    }
}
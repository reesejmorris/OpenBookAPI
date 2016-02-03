using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;

namespace OpenBookAPI.HttpCache{
    public class CacheStore : ICacheStore
    {
        public CacheStore(){
            _data = new List<CacheEntry>();
        }
        private readonly List<CacheEntry> _data; 
        public async Task<string> Get(string Url)
        {
            return _data.FirstOrDefault(x=>x.Url == Url && x.ValidUntil > DateTime.Now)?.ResponseBody;
        }
        public async Task Invalidate(string Url)
        {
            foreach (var item in _data.Where(x => x.Url.StartsWith(Url.Split('|').First())))
            {
                item.ValidUntil = DateTime.Now.AddHours(-1);
            }
        }

        public async Task Set(string Url, string ResponseBody, DateTime ValidUntil)
        {
            var entry = new CacheEntry{
                ResponseBody = ResponseBody,
                ValidUntil = ValidUntil,
                Url = Url
            };
            _data.Add(entry);
        }
    }
}
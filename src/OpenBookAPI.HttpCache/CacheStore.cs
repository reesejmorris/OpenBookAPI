using System;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace OpenBookAPI.HttpCache{
    public class CacheStore : ICacheStore
    {
        public CacheStore(){
            _data = new Dictionary<string,CacheEntry>();
        }
        private readonly Dictionary<string,CacheEntry> _data; 
        public async Task<string> Get(string Url)
        {
            CacheEntry entry;
            if(_data.TryGetValue(Url, out entry)&&entry.ValidUntil>DateTime.Now)
                return entry.ResponseBody;
            return null;
        }
        public async Task Invalidate(string Url)
        {
            _data.Remove(Url);
        }

        public async Task Set(string Url, string ResponseBody, DateTime ValidUntil)
        {
            var entry = new CacheEntry{
                ResponseBody = ResponseBody,
                ValidUntil = ValidUntil,
                Url = Url
            };
            _data.Add(Url,entry);
        }
    }
}
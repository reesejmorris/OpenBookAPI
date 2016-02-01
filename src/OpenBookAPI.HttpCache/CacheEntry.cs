using System;
namespace OpenBookAPI.HttpCache{
    public class CacheEntry{
        public string Url { get; set; }
        public string ResponseBody { get; set; }
        public DateTime ValidUntil { get; set; }
    }
}
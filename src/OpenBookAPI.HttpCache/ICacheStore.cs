using System;
using System.Threading.Tasks;
namespace OpenBookAPI.HttpCache{
    public interface ICacheStore{
        Task<string> Get(string Url);
        Task Set(string Url, string responseBody, DateTime ValidUntil);
        Task Invalidate(string Url);
    }
}
using System;
using System.Threading.Tasks;
using OpenBookAPI.Models;

namespace OpenBookAPI.Logic.Interfaces
{
    public interface IFlagProvider
    {
        Task<int> FlagSnippet(Guid id);
        Task<int> FlagSnippet(Flag flag);
        Task<int> FlagSnippet(Guid id, string reason);
        Task<int> UnFlagSnippet(Guid id);
    }
}
using System;
using OpenBookAPI.Models;
using System.Threading.Tasks;

namespace OpenBookAPI.Logic.Interfaces
{
    public interface IVoteProvider
    {
        Task<int> DownVote(Guid ItemId);
        Task<int> RegisterVote(Vote newVote);
        Task<int> RegisterVote(Guid ItemId, int value);
        Task<int> UpVote(Guid ItemId);
    }
}
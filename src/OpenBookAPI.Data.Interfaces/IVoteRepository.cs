using System;
using System.Collections.Generic;
using OpenBookAPI.Models;
using System.Threading.Tasks;

namespace OpenBookAPI.Data.Interfaces
{
    public interface IVoteRepository
    {
        Task<IEnumerable<Vote>> GetAll();
        Task<Vote> GetById(Guid voteId);
        Task<IEnumerable<Vote>> GetByItemId(Guid itemId);
        Task<Vote> CreateVote(Vote newVote);
    }
}
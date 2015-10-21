using System;
using System.Collections.Generic;
using OpenBookAPI.Models;

namespace OpenBookAPI.Data.Interfaces
{
    public interface IVoteRepository
    {
        IEnumerable<Vote> GetAll();
        Vote GetById(Guid voteId);
        IEnumerable<Vote> GetByItemId(Guid itemId);
        Vote CreateVote(Vote newVote);
    }
}
using System;
using OpenBookAPI.Models;

namespace OpenBookAPI.Logic.Interfaces
{
    public interface IVoteProvider
    {
        int DownVote(Guid ItemId);
        int RegisterVote(Vote newVote);
        int RegisterVote(Guid ItemId, int value);
        int UpVote(Guid ItemId);
    }
}
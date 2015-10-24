using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Logic.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Logic
{
    /// <summary>
    /// Vote provider to allow votes to be registered against items
    /// MVP1: Snippets only 
    /// MVP2: refactor to abstract factory to allow voting against different items
    /// </summary>
    public class VoteProvider : IVoteProvider
    {
        private readonly ISnippetRepository _snippetRepository;
        private readonly IVoteRepository _voteRepository;
        private readonly int voteValue = 1; // maybe give access to this in an options class that can be passed in via the constructor
        public VoteProvider(IVoteRepository voteRepo, ISnippetRepository snippetRepo)
        {
            _voteRepository = voteRepo;
            _snippetRepository = snippetRepo;
        }
        public async Task<int> DownVote(Guid ItemId)
        {
            return await RegisterVote(ItemId, -voteValue);
        }
        public async Task<int> UpVote(Guid ItemId)
        {
            return await RegisterVote(ItemId, voteValue);
        }

        public async Task<int> RegisterVote(Guid ItemId, int value)
        {
            return await RegisterVote(new Vote
            {
                ItemId = ItemId,
                Value = value,
                UserId = Guid.NewGuid()    //get from base 
            });
        }

        /// <summary>
        /// register a new vote on an snippet
        /// </summary>
        /// <param name="newVote"></param>
        /// <returns></returns>
        public async Task<int> RegisterVote(Vote newVote)
        {
            var snippet = await _snippetRepository.GetById(newVote.ItemId);
            if (snippet == null)
                return 0;  //No Snippet return zero

            var existingVotes = await _voteRepository.GetByItemId(newVote.ItemId);
            if (existingVotes.Count(v => v.UserId == newVote.UserId) > 0)
                return snippet.Score;  //Already voted return current score

            //create the vote
            var created = await _voteRepository.CreateVote(newVote);
            if (created.Id != Guid.Empty)
            {
                //update the score on the snippet model
                var voteCount = _voteRepository.GetByItemId(snippet.Id).Result.Sum(v => v.Value);
                if (voteCount != snippet.Score)
                {
                    snippet.Score = voteCount;
                }
                snippet = await _snippetRepository.Update(snippet);
            }
            return snippet.Score;
        }
    }
}

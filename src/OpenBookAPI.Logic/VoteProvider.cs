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
        public int DownVote(Guid ItemId)
        {
            return RegisterVote(ItemId, -voteValue);
        }
        public int UpVote(Guid ItemId)
        {
            return RegisterVote(ItemId, voteValue);
        }

        public int RegisterVote(Guid ItemId, int value)
        {
            return RegisterVote(new Vote
            {
                ItemId = ItemId,
                Value = value,
                UserId = new Guid(),    //get from base 
                CookieId = string.Empty,//^^^^^^^^^^^^^
            });
        }

        /// <summary>
        /// register a new vote on an snippet
        /// </summary>
        /// <param name="newVote"></param>
        /// <returns></returns>
        public int RegisterVote(Vote newVote)
        {
            var snippet = _snippetRepository.GetById(newVote.ItemId);
            if (snippet == null)
                return 0;  //No Snippet return zero
            
            if (_voteRepository.GetByItemId(newVote.ItemId).Count(v => (v.UserId == newVote.UserId) || (v.CookieId == newVote.CookieId)) > 0)
                return snippet.Score;  //Already voted return current score

            //create the vote
            var created = _voteRepository.CreateVote(newVote).Id != Guid.Empty; 
            if (created)
            {
                //update the score on the snippet model
                var voteCount = _voteRepository.GetByItemId(snippet.Id).Sum(v => v.Value);
                if (voteCount != snippet.Score)
                {
                    snippet.Score = voteCount;
                }
                snippet = _snippetRepository.Update(snippet);
            }
            return snippet.Score;
        }
    }
}

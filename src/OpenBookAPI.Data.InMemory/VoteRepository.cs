using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Data.InMemory
{
    public class VoteRepository : IVoteRepository
    {
        private List<Vote> _dataContext;
        public VoteRepository()
        {
            _dataContext = new List<Vote>
            {
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = 1
                },
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = 1
                },
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = 1
                },
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = 1
                },
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = 1
                },
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = -1
                },
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = 1
                },
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = -1
                },
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = 1
                },
                new Vote
                {
                    Id = Guid.NewGuid(),
                    ItemId = new Guid("ba0088dd-587d-4e16-a338-2446abfe459b"),
                    UserId = Guid.NewGuid(),
                    Value = -1
                },

            };

        }
        public async Task<Vote> GetById(Guid voteId)
        {
            return _dataContext.FirstOrDefault(s => s.Id == voteId);
        }

        public async Task<IEnumerable<Vote>> GetAll()
        {
            return _dataContext;
        }
        public async Task<IEnumerable<Vote>> GetByItemId(Guid itemId)
        {
            return _dataContext.Where(a => a.ItemId == itemId);
        }

        public async Task<Vote> CreateVote(Vote newVote)
        {
            newVote.Id = Guid.NewGuid();
            _dataContext.Add(newVote);
            return newVote;
        }
    }
}

using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Logic.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Logic
{
    public class FlagProvider : IFlagProvider
    {
        private readonly IFlagRepository _flagRepository;
        private readonly ISnippetRepository _snippetRepository;

        public FlagProvider(IFlagRepository flagRepository, ISnippetRepository snippetRepository)
        {
            _flagRepository = flagRepository;
            _snippetRepository = snippetRepository;
        }
        
        public async Task<int> FlagSnippet(Guid id)
        {
            return await FlagSnippet(id,"Unspecified");
        }
        public async Task<int> FlagSnippet(Guid id, string reason)
        {
            var flag = new Flag
            {
                ItemId = id,
                Reason = reason,
                UserId = new Guid(), //TODO get from base?
            };
            return await FlagSnippet(flag);
        }
        public async Task<int> FlagSnippet(Flag flag)
        {
            var getSnippet = _snippetRepository.GetById(flag.ItemId);
            var existing = _flagRepository.GetAll().Where(f => f.ItemId == flag.ItemId && f.UserId== flag.UserId);
            if(existing.Count() == 0)
            {
                flag = _flagRepository.CreateFlag(flag);
            }
            var snippet = await getSnippet;
            snippet.Flags = _flagRepository.GetAll().Where(f => f.ItemId == flag.ItemId).Count();
            var res = await _snippetRepository.Update(snippet);

            return res.Flags;
        }
        public async Task<int> UnFlagSnippet(Guid id)
        {
            var getSnippet = _snippetRepository.GetById(id);
            var flag = _flagRepository.GetAll().FirstOrDefault(f=>f.ItemId == id);
            var snippet = await getSnippet;
            if (flag != null)
            {
                _flagRepository.DeleteFlag(flag.Id);
                snippet.Flags = _flagRepository.GetAll().Where(f => f.ItemId == flag.ItemId).Count();
                snippet = await _snippetRepository.Update(snippet);
            }
            return snippet.Flags;
        }
    }
}

using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Logic
{
    public class FlagProvider
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
            var getSnippet = _snippetRepository.GetById(id);
            var existing = _flagRepository.GetAll().Where(f => f.ItemId == id);
            if(existing.Count() == 0)
            {
                var flag = new Flag
                {
                    ItemId = id,
                    Reason = "Unspecified",
                    UserId = new Guid(), //TODO get from base?
                };
                flag = _flagRepository.CreateFlag(flag);
            }
            var snippet = await getSnippet;
            snippet.Flags = _flagRepository.GetAll().Where(f => f.ItemId == id).Count();
            var res = await _snippetRepository.Update(snippet);

            return res.Flags;
        }
        public async Task<int> UnFlagSnippet(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}

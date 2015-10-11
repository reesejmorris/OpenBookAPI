using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenBookAPI.Logic.Interfaces;
using OpenBookAPI.Data.Interfaces;

namespace OpenBookAPI.Logic
{
    public class SnippetProvider : ISnippetProvider
    {
        private readonly ISnippetRepository _repository;
        public SnippetProvider(ISnippetRepository repository)
        {
            _repository = repository;
        }
        public ISnippet GetSnippet(Guid id)
        {
            return _repository.GetById(id);
        }

        public IEnumerable<ISnippet> GetStorySoFar()
        {
            var currentStory = new Guid();
            return _repository.GetByStory(currentStory);
        }
    }
}

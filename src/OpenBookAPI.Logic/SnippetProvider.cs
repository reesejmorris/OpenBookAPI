using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenBookAPI.Logic.Interfaces;
using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Models;

namespace OpenBookAPI.Logic
{
    public class SnippetProvider : ISnippetProvider
    {
        private readonly ISnippetRepository _repository;
       
        public SnippetProvider(ISnippetRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Snippet> GetSnippets()
        {
            return _repository.GetAll();
        }
        public Snippet GetSnippet(Guid id)
        {
            return _repository.GetById(id);
        }

        public Snippet SubmitSnippet(Snippet snippet)
        {
            return _repository.Create(snippet);
        }

        public Snippet UpdateSnippet(Snippet snippet)
        {
            return _repository.Update(snippet);
        }

        public bool DeleteSnippet(Guid id)
        {
            return _repository.Delete(id);
        }
    }
}

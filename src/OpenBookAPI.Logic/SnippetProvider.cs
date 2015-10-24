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
        public async Task<IEnumerable<Snippet>> GetSnippetsForStory(Guid storyId)
        {
            return await _repository.GetByStory(storyId);
        }
        public async Task<IEnumerable<Snippet>> GetChosenSnippetsForStory(Guid storyId)
        {
            var snippets = await _repository.GetByStory(storyId);
            return snippets.Where(s=>s.Status == SnippetStatus.Chosen);
        }
        public async Task<IEnumerable<Snippet>> GetSnippets()
        {
            return await _repository.GetAll();
        }
        public async Task<Snippet> GetSnippet(Guid id)
        {
            return await _repository.GetById(id);
        }

        public async Task<Snippet> SubmitSnippet(Snippet snippet)
        {
            return await _repository.Create(snippet);
        }

        public async Task<Snippet> UpdateSnippet(Snippet snippet)
        {
            return await _repository.Update(snippet);
        }

        public async Task<bool> DeleteSnippet(Guid id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<Snippet>> GetSnippetsForSubmissionPeriod(Guid submissionPeriodId)
        {
            return await _repository.GetBySubmissionPeriodId(submissionPeriodId);
        }

        public async Task<Snippet> FlagSnippet(Guid snippetId)
        {
            var snippet = await _repository.GetById(snippetId);
            snippet.Flags += 1;
            return await _repository.Update(snippet);
        }
    }
}

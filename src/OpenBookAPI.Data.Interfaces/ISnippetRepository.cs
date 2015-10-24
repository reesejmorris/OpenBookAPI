using System;
using OpenBookAPI.Logic.Interfaces;
using System.Collections.Generic;
using OpenBookAPI.Models;
using System.Threading.Tasks;

namespace OpenBookAPI.Data.Interfaces
{
    public interface ISnippetRepository
    {
        Task<Snippet> GetById(Guid id);
        Task<IEnumerable<Snippet>> GetAll();
        Task<IEnumerable<Snippet>> GetByStory(Guid id);
        Task<IEnumerable<Snippet>> GetBySubmissionPeriodId(Guid id);
        Task<Snippet> Create(Snippet snippet);
        Task<Snippet> Update(Snippet snippet);
        Task<bool> Delete(Guid id);
    }
}
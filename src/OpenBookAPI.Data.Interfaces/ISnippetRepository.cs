using System;
using OpenBookAPI.Logic.Interfaces;
using System.Collections.Generic;
using OpenBookAPI.Models;

namespace OpenBookAPI.Data.Interfaces
{
    public interface ISnippetRepository
    {
        Snippet GetById(Guid id);
        IEnumerable<Snippet> GetAll();
        IEnumerable<Snippet> GetByStory(Guid id);
        IEnumerable<Snippet> GetBySubmissionPeriodId(Guid id);
        Snippet Create(Snippet snippet);
        Snippet Update(Snippet snippet);
        bool Delete(Guid id);
    }
}
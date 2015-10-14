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
        IEnumerable<Snippet> GetByStory(Guid Id);
        IEnumerable<Snippet> GetBySubmissionPeriodId(Guid Id);
    }
}
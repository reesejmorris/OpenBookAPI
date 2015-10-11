using System;
using OpenBookAPI.Logic.Interfaces;
using System.Collections.Generic;

namespace OpenBookAPI.Data.Interfaces
{
    public interface ISnippetRepository
    {
        ISnippet GetById(Guid id);
        IEnumerable<ISnippet> GetByStory(Guid Id);
        IEnumerable<ISnippet> GetSubmissionsByStory(Guid Id);
    }
}
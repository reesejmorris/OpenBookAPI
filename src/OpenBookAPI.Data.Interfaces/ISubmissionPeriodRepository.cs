using System;
using System.Collections.Generic;
using OpenBookAPI.Models;

namespace OpenBookAPI.Data.InMemory
{
    public interface ISubmissionPeriodRepository
    {
        SubmissionPeriod GetById(Guid submissionPeriodId);
        IEnumerable<SubmissionPeriod> GetByStoryId(Guid storyId);
    }
}
using System;
using System.Collections.Generic;
using OpenBookAPI.Models;

namespace OpenBookAPI.Data.Interfaces
{
    public interface ISubmissionPeriodRepository
    {
        SubmissionPeriod GetById(Guid submissionPeriodId);
        IEnumerable<SubmissionPeriod> GetAll();
        IEnumerable<SubmissionPeriod> GetByStoryId(Guid storyId);
    }
}
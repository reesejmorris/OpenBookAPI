using System;
using System.Collections.Generic;
using OpenBookAPI.Models;

namespace OpenBookAPI.Data.Interfaces
{
    public interface ISubmissionPeriodProvider
    {
        SubmissionPeriod GetSubmissionPeriod(Guid id);
        IEnumerable<SubmissionPeriod> GetSubmissionPeriods();
    }
}
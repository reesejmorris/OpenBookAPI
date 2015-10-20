using System;
using System.Collections.Generic;
using OpenBookAPI.Models;

namespace OpenBookAPI.Logic.Interfaces
{
    public interface ISubmissionPeriodProvider
    {
        SubmissionPeriod GetSubmissionPeriod(Guid id);
        IEnumerable<SubmissionPeriod> GetSubmissionPeriods();
    }
}
using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Logic.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Logic
{
    public class SubmissionPeriodProvider : ISubmissionPeriodProvider
    {
        public SubmissionPeriodProvider(ISubmissionPeriodRepository repository)
        {
            _repository = repository;
        }

        private readonly ISubmissionPeriodRepository _repository;

        public IEnumerable<SubmissionPeriod> GetSubmissionPeriods()
        {
            return _repository.GetAll();
        }

        public SubmissionPeriod GetCurrentSubmissionPeriodForStory(Guid storyId)
        {
            return _repository.GetByStoryId(storyId).OrderBy(x => x.StartDate).FirstOrDefault(x=>x.Status == SubmissionPeriodStatus.Open);
        }

        public SubmissionPeriod GetSubmissionPeriod(Guid id)
        {
            return _repository.GetById(id);
        }

        public SubmissionPeriod GetCurrentForStory(Guid storyId)
        {
            return _repository.GetByStoryId(storyId).OrderByDescending(x=>x.StartDate).FirstOrDefault(x=>x.Status == SubmissionPeriodStatus.Open);
        }
    }
}

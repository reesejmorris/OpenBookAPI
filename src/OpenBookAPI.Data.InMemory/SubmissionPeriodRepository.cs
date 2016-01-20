using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Data.InMemory
{
    public class SubmissionPeriodRepository : ISubmissionPeriodRepository
    {
        private List<SubmissionPeriod> _dataContext;
        public SubmissionPeriodRepository()
        {
            _dataContext = new List<SubmissionPeriod>
            {
                new SubmissionPeriod
                {
                    Id = new Guid("3f6945fe-83e4-478b-8dd4-9ffbc66a9f35"),
                    StoryId = new Guid("8e733419-c6a3-4b59-8d5a-8784c1b61724"),
                    StartDate = new DateTime(2015,11,01),
                    Status = SubmissionPeriodStatus.Closed,
                    Location = 0
                },
                new SubmissionPeriod
                {
                    Id = new Guid("3f95f457-6caf-4fc6-a266-1a9ebdab4cf6"),
                    StoryId = new Guid("8e733419-c6a3-4b59-8d5a-8784c1b61724"),
                    StartDate = new DateTime(2015,11,02),
                    Status = SubmissionPeriodStatus.Open,
                    Location = 0
                }
            };
        }
        public SubmissionPeriod GetById(Guid submissionPeriodId)
        {
            return _dataContext.FirstOrDefault(sp=>sp.Id == submissionPeriodId);
        }

        public IEnumerable<SubmissionPeriod> GetAll()
        {
            return _dataContext;
        }

        public IEnumerable<SubmissionPeriod> GetByStoryId(Guid storyId)
        {
            return _dataContext.Where(sp => sp.StoryId == storyId);
        }
    }
}

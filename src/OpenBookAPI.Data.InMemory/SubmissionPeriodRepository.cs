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
        private IEnumerable<SubmissionPeriod> _dataContext;
        public SubmissionPeriodRepository()
        {
            _dataContext = new[]
            {
                new SubmissionPeriod
                {
                    Id = new Guid("3f6945fe-83e4-478b-8dd4-9ffbc66a9f35"),
                    StoryId = new Guid("8e733419-c6a3-4b59-8d5a-8784c1b61724"),
                    Location = 0,
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

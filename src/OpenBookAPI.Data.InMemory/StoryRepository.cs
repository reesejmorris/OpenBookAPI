using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OpenBookAPI.Data.InMemory
{
    public class StoryRepository : IStoryRepository
    {
        private List<Story> _dataContext;
        public StoryRepository()
        {
            _dataContext = new List<Story>
            {
                new Story
                {
                    Id = new Guid("8e733419-c6a3-4b59-8d5a-8784c1b61724"),
                    StartDate = new DateTime(2015,9,14,12,18,00),
                    Title = "The first ever story",
                    Status = StoryStatus.Open
                },
                new Story
                {
                    Id = new Guid("537aaabe-1370-455e-9921-90b2ba81cc44"),
                    StartDate = new DateTime(2015,10,14,12,18,00),
                    Title = "The Second ever story",
                    Status = StoryStatus.Draft
                }
            };
        }
        public Story GetById(Guid id)
        {
            return _dataContext.FirstOrDefault(s => s.Id == id);
        }

        public IEnumerable<Story> GetAll()
        {
            return _dataContext;
        }
    }
}

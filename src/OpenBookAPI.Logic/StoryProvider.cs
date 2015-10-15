using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OpenBookAPI.Logic.Interfaces;
using OpenBookAPI.Data.Interfaces;
using OpenBookAPI.Models;

namespace OpenBookAPI.Logic
{
    public class StoryProvider : IStoryProvider
    {
        private readonly IStoryRepository _repository;
       
        public StoryProvider(IStoryRepository repository)
        {
            _repository = repository;
        }
        public IEnumerable<Story> GetStories()
        {
            return _repository.GetAll();
        }
        public Story GetStory(Guid id)
        {
            return _repository.GetById(id);
        }
        
    }
}

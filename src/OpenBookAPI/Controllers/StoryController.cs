using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OpenBookAPI.Models;
using OpenBookAPI.Logic.Interfaces;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenBookAPI.Controllers
{
    [Route("api/[controller]")]
    public class StoryController : Controller
    {
        private IStoryProvider StoryProvider { get; set; }

        public StoryController(IStoryProvider provider)
        {
            StoryProvider = provider;
        }

        // GET: api/Story
        [HttpGet]
        public IEnumerable<Story> Get()
        {
            return StoryProvider.GetStories();
        }

        // GET: api/Story/{id:Guid}
        [HttpGet("{id:Guid}")]
        public Story Get(Guid id)
        {
            return StoryProvider.GetStory(id);
        }

    }
}

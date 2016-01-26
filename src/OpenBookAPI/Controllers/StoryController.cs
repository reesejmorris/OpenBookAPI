using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OpenBookAPI.Models;
using OpenBookAPI.Logic.Interfaces;
using OpenBookAPI.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenBookAPI.Controllers
{
    [Route("api/[controller]")]
    public class StoryController : Controller
    {
        private IStoryProvider StoryProvider { get;}
        private ISnippetProvider SnippetProvider { get;}

        private ISubmissionPeriodProvider SubmissionPeriodProvider { get; }

        public StoryController(IStoryProvider provider, ISnippetProvider snippetProvider, ISubmissionPeriodProvider submissionPeriodProvider)
        {
            StoryProvider = provider;
            SnippetProvider = snippetProvider;
            SubmissionPeriodProvider = submissionPeriodProvider;
        }

        // GET: api/Story
        [HttpGet]
        public IEnumerable<Story> Get()
        {
            return StoryProvider.GetStories();
        }
        // GET: api/Story/Latest
        [HttpGet("latest")]
        public Story GetLatest()
        {
            return StoryProvider.GetLatest();
        }


        // GET: api/Story/{id:Guid}/SoFar
        [HttpGet("{id:Guid}/SoFar")]
        public async Task<StorySoFar> GetStorySoFar(Guid id)
        {
            var story = StoryProvider.GetStory(id);
            var chosenSnippets = await SnippetProvider.GetChosenSnippetsForStory(id);
            var submission = SubmissionPeriodProvider.GetCurrentSubmissionPeriodForStory(id);
            var submittedSnippets = await SnippetProvider.GetSnippetsForSubmissionPeriod(submission.Id);
            var storySoFar = new StorySoFar
            {
                ChosenSnippets = chosenSnippets,
                CurrentSubmissionPeriod = submission,
                Story = story,
                SubmittedSnippets = submittedSnippets
            };
            return storySoFar;
        }

        // GET: api/Story/{id:Guid}
        [HttpGet("{id:Guid}")]
        public Story Get(Guid id)
        {
            return StoryProvider.GetStory(id);
        }

    }
}

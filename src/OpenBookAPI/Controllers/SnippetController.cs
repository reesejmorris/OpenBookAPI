using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using OpenBookAPI.Logic.Interfaces;
using System;
using OpenBookAPI.Models;
using Microsoft.AspNet.Cors;
using Microsoft.AspNet.Authorization;
using System.Threading.Tasks;

namespace OpenBookAPI.Controllers
{
    [EnableCors("OpenBookAPI")]
    [Route("api/[controller]")]
    public class SnippetController : Controller
    {
        private ISnippetProvider snippetProvider { get; set; }
        private IVoteProvider voteProvider { get; set; }
        private IFlagProvider flagProvider { get; set; }
        /// <summary>
        /// Snippet Controller constructor
        /// </summary>
        /// <param name="provider">The injected <see cref="ISnippetProvider"/></param>
        public SnippetController(ISnippetProvider snippetprovider, IVoteProvider voteprovider, IFlagProvider flagprovider)
        {
            snippetProvider = snippetprovider;
            voteProvider = voteprovider;
            flagProvider = flagprovider;
        }

        /// <summary>
        /// Gets all the <see cref="Snippet"/>s
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Snippet"/>s</returns>
        // GET: api/Snippet
        [HttpGet]
        async public Task<IEnumerable<Snippet>> Get()
        {
            return await snippetProvider.GetSnippets();// snippetProvider.GetSnippets();
        }

        /// <summary>
        /// Get a <see cref="Snippet"/> by Id
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> of the desired <see cref="Snippet"/></param>
        /// <returns>The requested <see cref="Snippet"/></returns>
        // GET api/snippet/{id:Guid}
        [HttpGet("{id:Guid}")]
        async public Task<Snippet> Get(Guid id)
        {
            return await snippetProvider.GetSnippet(id);
        }

        /// <summary>
        /// Gets all the <see cref="Snippet"/>s for a story
        /// </summary>
        /// <param name="storyId">The <see cref="Guid"/> of the story</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Snippet"/>s</returns>
        // GET api/story/{story_id:Guid}/snippet
        [HttpGet("~/api/story/{storyId:Guid}/ChosenSnippets")]
        async public Task<IEnumerable<Snippet>> GetChosenByStoryId(Guid storyId)
        {
            return await snippetProvider.GetChosenSnippetsForStory(storyId);
        }


        /// <summary>
        /// Flag a snippet as inapropriate
        /// </summary>
        /// <param name="snippetId"></param>
        /// <returns>An <see cref="int"/> signifying the number of flags a snippet has</returns>
        [HttpPost("{snippetId:Guid}/Flag")]
        async public Task<int> FlagSnippetAsInappropriate(Guid snippetId)
        {
            return await flagProvider.FlagSnippet(snippetId);
        }

        /// <summary>
        /// UnFlag a snippet
        /// </summary>
        /// <param name="snippetId"></param>
        /// <returns>An <see cref="int"/> signifying the number of flags a snippet has</returns>
        [HttpPost("{snippetId:Guid}/UnFlag")]
        async public Task<int> UnFlagSnippet(Guid snippetId)
        {
            return await flagProvider.UnFlagSnippet(snippetId);
        }

        /// <summary>
        /// Gets all the <see cref="Snippet"/>s for a submission period
        /// </summary>
        /// <param name="submissionPeriodId">The <see cref="Guid"/> of the submission period</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Snippet"/>s</returns>
        // GET api/story/{story_id:Guid}/snippet
        [HttpGet("~/api/submissionperiod/{submissionPeriodId:Guid}/snippet")]
        async public Task<IEnumerable<Snippet>> GetBySubmissionPeriodId(Guid submissionPeriodId)
        {
            return await snippetProvider.GetSnippetsForSubmissionPeriod(submissionPeriodId);
        }

        /// <summary>
        /// Gets all the <see cref="Snippet"/>s for a story
        /// </summary>
        /// <param name="storyId">The <see cref="Guid"/> of the story</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Snippet"/>s</returns>
        // GET api/story/{story_id:Guid}/snippet
        [HttpGet("~/api/story/{storyId:Guid}/snippet")]
        async public Task<IEnumerable<Snippet>> GetByStoryId(Guid storyId)
        {
            return await snippetProvider.GetSnippetsForStory(storyId);
        }

        /// <summary>
        /// Submit a new <see cref="Snippet"/>
        /// </summary>
        /// <param name="snippet">The new <see cref="Snippet"/></param>
        /// <returns>The created <see cref="Snippet"/></returns>
        // POST api/snippet
        [HttpPost]
        [Authorize(ActiveAuthenticationSchemes = "Bearer")]
        async public Task<Snippet> Post([FromBody]Snippet snippet)
        {
            return await snippetProvider.SubmitSnippet(snippet);
        }

        /// <summary>
        /// Update a <see cref="Snippet"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="Snippet"/> to update</param>
        /// <param name="snippet">The updated <see cref="Snippet"/> model</param>
        /// <returns>The updated <see cref="Snippet"/></returns>
        // PUT api/Snippet/{id:Guid}
        [HttpPut("{id:Guid}")]
        async public Task<Snippet> Put(Guid id, [FromBody]Snippet snippet)
        {
            return await snippetProvider.UpdateSnippet(snippet);
        }

        /// <summary>
        /// Delete a <see cref="Snippet"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="Snippet"/> to delete</param>
        /// <returns>Success(<see cref="bool"/>)</returns>
        // DELETE api/Snippet/{id:Guid}
        [HttpDelete("{id:Guid}")]
        async public Task<bool> Delete(Guid id)
        {
            return await snippetProvider.DeleteSnippet(id);
        }
        /// <summary>
        /// Register a new downvote on a snippet (Set ContentLength header to 0 when posting to this endpoint)
        /// </summary>
        /// <param name="id">Snippet Id</param>
        /// <returns><see cref="int"/> Score</returns>
        [HttpPost("{id:Guid}/UpVote")]
        async public Task<int> UpVote(Guid id)
        {
            return await voteProvider.UpVote(id);
        }
        /// <summary>
        /// Register a new upvote on a snippet (Set ContentLength header to 0 when posting to this endpoint)
        /// </summary>
        /// <param name="id">Snippet Id</param>
        /// <returns><see cref="int"/> Score</returns>
        [HttpPost("{id:Guid}/DownVote")]
        async public Task<int> DownVote(Guid id)
        {
            return await voteProvider.DownVote(id);
        }
    }
}
    
using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using OpenBookAPI.Logic.Interfaces;
using System;
using OpenBookAPI.Models;
using Microsoft.AspNet.Cors.Core;

namespace OpenBookAPI.Controllers
{
    [EnableCors("OpenBookAPI")]
    [Route("api/[controller]")]
    public class SnippetController : Controller
    {
        private ISnippetProvider snippetProvider { get; set; }
        private IVoteProvider voteProvider { get; set; }
        /// <summary>
        /// Snippet Controller constructor
        /// </summary>
        /// <param name="provider">The injected <see cref="ISnippetProvider"/></param>
        public SnippetController(ISnippetProvider snippetprovider, IVoteProvider voteprovider)
        {
            snippetProvider = snippetprovider;
            voteProvider = voteprovider;
        }

        /// <summary>
        /// Gets all the <see cref="Snippet"/>s
        /// </summary>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Snippet"/>s</returns>
        // GET: api/Snippet
        [HttpGet]
        public IEnumerable<Snippet> Get()
        {
            return snippetProvider.GetSnippets();
        }

        /// <summary>
        /// Get a <see cref="Snippet"/> by Id
        /// </summary>
        /// <param name="id">The <see cref="Guid"/> of the desired <see cref="Snippet"/></param>
        /// <returns>The requested <see cref="Snippet"/></returns>
        // GET api/snippet/{id:Guid}
        [HttpGet("{id:Guid}")]
        public Snippet Get(Guid id)
        {
            return snippetProvider.GetSnippet(id);
        }
        /// <summary>
        /// Gets all the <see cref="Snippet"/>s for a story
        /// </summary>
        /// <param name="story_id">The <see cref="Guid"/> of the story</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Snippet"/>s</returns>
        // GET api/story/{story_id:Guid}/snippet
        [HttpGet("~/api/story/{story_id:Guid}/snippet")]
        public IEnumerable<Snippet> GetByStoryId(Guid story_id)
        {
            return snippetProvider.GetSnippetsForStory(story_id);
        }

        /// <summary>
        /// Gets all the <see cref="Snippet"/>s for a story
        /// </summary>
        /// <param name="story_id">The <see cref="Guid"/> of the story</param>
        /// <returns>An <see cref="IEnumerable{T}"/> of <see cref="Snippet"/>s</returns>
        // GET api/story/{story_id:Guid}/snippet
        [HttpGet("~/api/submissionperiod/{submissionPeriodId:Guid}/snippet")]
        public IEnumerable<Snippet> GetBySubmissionPeriodId(Guid submissionPeriodId)
        {
            return snippetProvider.GetSnippetsForSubmissionPeriod(submissionPeriodId);
        }

        /// <summary>
        /// Submit a new <see cref="Snippet"/>
        /// </summary>
        /// <param name="snippet">The new <see cref="Snippet"/></param>
        /// <returns>The created <see cref="Snippet"/></returns>
        // POST api/snippet
        [HttpPost]
        public Snippet Post([FromBody]Snippet snippet)
        {
            return snippetProvider.SubmitSnippet(snippet);
        }

        /// <summary>
        /// Update a <see cref="Snippet"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="Snippet"/> to update</param>
        /// <param name="snippet">The updated <see cref="Snippet"/> model</param>
        /// <returns>The updated <see cref="Snippet"/></returns>
        // PUT api/Snippet/{id:Guid}
        [HttpPut("{id:Guid}")]
        public Snippet Put(Guid id, [FromBody]Snippet snippet)
        {
            return snippetProvider.UpdateSnippet(snippet);
        }

        /// <summary>
        /// Delete a <see cref="Snippet"/>
        /// </summary>
        /// <param name="id">The id of the <see cref="Snippet"/> to delete</param>
        /// <returns>Success(<see cref="bool"/>)</returns>
        // DELETE api/Snippet/{id:Guid}
        [HttpDelete("{id:Guid}")]
        public bool Delete(Guid id)
        {
            return snippetProvider.DeleteSnippet(id);
        }
        /// <summary>
        /// Register a new downvote on a snippet (Set ContentLength header to 0 when posting to this endpoint)
        /// </summary>
        /// <param name="id">Snippet Id</param>
        /// <returns><see cref="int"/> Score</returns>
        [HttpPost("{id:Guid}/UpVote")]
        public int UpVote(Guid id)
        {
            return voteProvider.UpVote(id);
        }
        /// <summary>
        /// Register a new upvote on a snippet (Set ContentLength header to 0 when posting to this endpoint)
        /// </summary>
        /// <param name="id">Snippet Id</param>
        /// <returns><see cref="int"/> Score</returns>
        [HttpPost("{id:Guid}/DownVote")]
        public int DownVote(Guid id)
        {
            return voteProvider.DownVote(id);
        }
    }
}
    
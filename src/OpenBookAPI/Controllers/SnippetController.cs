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
        private ISnippetProvider SnippetProvider { get; set; }

        public SnippetController(ISnippetProvider provider)
        {
            SnippetProvider = provider;
        }

        // GET: api/Snippet
        [HttpGet]
        public IEnumerable<Snippet> Get()
        {
            return SnippetProvider.GetSnippets();
        }

        // GET api/Snippet/{id:Guid}
        [HttpGet("{id:Guid}")]
        public Snippet Get(Guid id)
        {
            return SnippetProvider.GetSnippet(id);
        }

        // POST api/Snippet
        [HttpPost]
        public Snippet Post([FromBody]Snippet snippet)
        {
            return SnippetProvider.SubmitSnippet(snippet);
        }

        // PUT api/Snippet/{id:Guid}
        [HttpPut("{id:Guid}")]
        public Snippet Put(Guid id, [FromBody]Snippet snippet)
        {
            return SnippetProvider.UpdateSnippet(snippet);
        }

        // DELETE api/Snippet/{id:Guid}
        [HttpDelete("{id:Guid}")]
        public bool Delete(Guid id)
        {
            return SnippetProvider.DeleteSnippet(id);
        }
    }
}

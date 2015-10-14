using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using OpenBookAPI.Logic.Interfaces;
using System;
using OpenBookAPI.Models;

namespace OpenBookAPI.Controllers
{
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

        // GET api/Snippet/5
        [HttpGet("{id}")]
        public Snippet Get(Guid id)
        {
            return SnippetProvider.GetSnippet(id);
        }

        // POST api/Snippet
        [HttpPost]
        public void Post([FromBody]Snippet value)
        {
        }

        // PUT api/Snippet/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/Snippet/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

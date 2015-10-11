using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using OpenBookAPI.Logic.Interfaces;
using System;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

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
        public IEnumerable<ISnippet> Get()
        {
            
            return SnippetProvider.GetStorySoFar();
        }

        // GET api/Snippet/5
        [HttpGet("{id}")]
        public ISnippet Get(Guid id)
        {
            return SnippetProvider.GetSnippet(id);
        }

        // POST api/Snippet
        [HttpPost]
        public void Post([FromBody]ISnippet value)
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

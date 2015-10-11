using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using OpenBookAPI.Models;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenBookAPI.Controllers
{
    [Route("api/[controller]")]
    public class SnippetController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Snippet> Get()
        {
            return new List<Snippet> { new Snippet { SnippetContent = "Here is a snippet of a story, good times!", SnippetAuthor = "Bob David" } };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

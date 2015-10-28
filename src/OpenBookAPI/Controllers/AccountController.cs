using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Cors.Core;
using System.Net.Http;
using OpenBookAPI.Models;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenBookAPI.Controllers
{
    [Authorize, Route("api/[controller]"), EnableCors("OpenBookAPI")]
    public class AccountController : Controller
    {
        /// <summary>
        /// Gets the currently logged in author
        /// </summary>
        /// <returns>the logged in <see cref="Author"/></returns>
        [HttpGet]
        public Author Get()
        {
            return new Author
            {
                Id = new Guid(User.FindFirst("_id").Value),
                Image = User.FindFirst("picture").Value,
                Name = User.Identity.Name,
            };
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Cors.Core;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenBookAPI.Controllers
{
    [Authorize, Route("api/[controller]"), EnableCors("OpenBookAPI")]
    public class AccountController : Controller
    {
        
        [HttpGet]
        
        public int Get()
        {
            return 200;
        }
    }
}

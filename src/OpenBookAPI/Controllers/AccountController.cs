using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Authorization;
// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenBookAPI.Controllers
{
    [Authorize(ActiveAuthenticationSchemes = Microsoft.AspNet.Authentication.OpenIdConnect.OpenIdConnectAuthenticationDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        
        [HttpGet]
        [Authorize]
        public int Get()
        {
            return 200;
        }
    }
}

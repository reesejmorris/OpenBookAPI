using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using OpenBookAPI.Models;
using OpenBookAPI.Logic;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace OpenBookAPI.Controllers
{
    [Route("api/[controller]")]
    public class SubmissionPeriodController : Controller
    {
        private readonly SubmissionPeriodProvider _provider;
        public SubmissionPeriodController(SubmissionPeriodProvider provider) {
            _provider = provider;
        }


        // GET: api/values
        [HttpGet]
        public IEnumerable<SubmissionPeriod> Get()
        {
            return _provider.GetSubmissionPeriods();
        }

        // GET api/values/{id:Guid}
        [HttpGet("{id:Guid}")]
        public SubmissionPeriod Get(Guid id)
        {
            return _provider.GetSubmissionPeriod(id);
        }
    }
}

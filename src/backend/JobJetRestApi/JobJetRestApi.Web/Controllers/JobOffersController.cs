using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JobJetRestApi.Web.Controllers
{
    [Route("api/job-offers")]
    public class JobOffersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public JobOffersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET api/job-offers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            return new string[] { "value1", "value2"};
        }
        
        // GET api/job-offers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> Get(int id)
        {
            return "value";
        }
        
        // POST api/job-offers
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] string value)
        {
            return 100;
        }
        
        // PUT api/job-offers/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string value)
        {
            
        }
        
        // DELETE api/job-offers/5
        public async Task Delete(int id)
        {
            
        }
    }
}
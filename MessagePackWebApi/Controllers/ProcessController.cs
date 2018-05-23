using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using Microsoft.AspNetCore.Mvc;
using Models.Processes;

namespace MessagePackWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessController : ControllerBase
    {
        IProcessProvider processesProvider;
        public ProcessController(IProcessProvider processesProvider)
        {
            this.processesProvider = processesProvider;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await processesProvider.Get((p) => true, 100, 0);

            if (result.Count() == 0)
                return NoContent();

            return Ok(result);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetProcess" )]
        public async Task<IActionResult> Get(int id)
        {
            var result = await processesProvider.Get(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Process value)
        {
            var result = await processesProvider.Create(value);

            if (!result)
                return Conflict();

            return Created(UrlHelperExtensions.Action(Url,"GetProcess"), value);
        }
        

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await processesProvider.Delete(id);

            if (!result)
                return BadRequest();

            return Ok();
        }
    }
}

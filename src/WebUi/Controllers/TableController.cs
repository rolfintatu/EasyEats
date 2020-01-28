using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Table.Commands.CreateTable;
using AppCore.Table.Queries.TableList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var results = await mediator.Send(new TablesListQuery());

            return Ok(results);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Create([FromBody] CreateTableCommand command)
        {
            await mediator.Send(command);

            return NoContent();
        }
    }
}
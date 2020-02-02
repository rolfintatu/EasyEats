using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Table.Commands.CreateTable;
using Application.Table.Commands.DeleteTable;
using Application.Table.Queries.TableDetails;
using Application.Table.Queries.TableList;
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

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var tableDetails = await mediator.Send(new TableDetailsQuery() { Id = id });

            return Ok(tableDetails);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Create([FromBody] CreateTableCommand command)
        {
            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteTableCommand() { Id = id });

            return NoContent();
        }

    }
}
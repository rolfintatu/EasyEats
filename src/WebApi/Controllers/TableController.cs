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

namespace WebApi.Controllers
{
    [Route("EasyApi/[controller]")]
    [ApiController]
    public class TableController : BaseController
    {
        //Queries
        [HttpGet]
        public async Task<IActionResult> GetAll()
        => Ok(await mediator.Send(new TablesListQuery()));
        

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get([FromRoute] int id)
        => Ok(await mediator.Send(new TableDetailsQuery(id)));

        //Commands
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task Create([FromBody] CreateTableCommand command)
        => await mediator.Send(command);


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete([FromBody] int id)
        => await mediator.Send(new DeleteTableCommand(id));

    }
}
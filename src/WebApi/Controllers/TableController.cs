using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("EasyApi/[controller]")]
    [ApiController]
    public class TableController : BaseController
    {
        //Queries
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //=> Ok(await mediator.Send(new TablesListQuery()));


        //[HttpGet("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task<IActionResult> Get([FromRoute] int id)
        //=> Ok(await mediator.Send(new TableDetailsQuery(id)));

        //[HttpGet("Availability")]
        //public async Task<IActionResult> Availability([FromQuery] int tableId, int day, int month, int year, int duration)
        //    => Ok(await mediator.Send(new AvailabilityRequest(tableId, new Domain.ValueObjects.Date(day, month, year), duration)));

        //Commands
        //[HttpPut]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //public async Task Create([FromBody] CreateTableCommand command)
        //=> await mediator.Send(command);


        //[HttpDelete("{id}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task Delete([FromBody] int id)
        //=> await mediator.Send(new DeleteTableCommand(id));

    }
}
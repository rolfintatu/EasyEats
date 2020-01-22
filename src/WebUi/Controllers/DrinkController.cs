using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Drink.Commands.CreateDrink;
using AppCore.Drink.Commands.DeleteDrink;
using AppCore.Drink.Commands.UpdateDrink;
using AppCore.Drink.Queries.DrinkDetails;
using AppCore.Drink.Queries.DrinkList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinkController : BaseController
    {

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var drinkList = await mediator.Send(new DrinkListQuery());

            return Ok(drinkList);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Get(int id)
        {
            var drinkDetails = await mediator.Send(new DrinkDetailsQuery() { Id = id });

            return Ok(drinkDetails);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateDrinkCommand command)
        {
            await mediator.Send(command);

            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateDrinkCommand command)
        {
            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteDrinkCommand() { Id = id });

            return NoContent();
        }
    }
}
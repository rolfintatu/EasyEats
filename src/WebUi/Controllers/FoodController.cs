using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Food.Commands.CreateFood;
using AppCore.Food.Commands.DeleteFood;
using AppCore.Food.Commands.UpdateFood;
using AppCore.Food.Queries.FoodDetails;
using AppCore.Food.Queries.GetFoods;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class FoodController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<FoodList>> GetAll()
        {
            var result = await mediator.Send(new FoodListRequest());

            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FoodDetails>> Get([FromQuery] int id)
        {
            var result = await mediator.Send(new FoodDetailsRequest() { Id = id });

            return result;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> Create([FromBody] CreateFoodCommand food)
        {
            await mediator.Send(food);

            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update([FromBody] UpdateFoodCommand updateFood)
        {
            await mediator.Send(updateFood);

            return NoContent();
        }

        [HttpDelete("{id}")]
        //[Route("api/[controller]/delete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            await mediator.Send(new DeleteFoodCommand() { Id = id });

            return NoContent();
        }
    }
}
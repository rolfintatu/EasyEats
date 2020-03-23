using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Commands = Application.OrderItem.Commands;
using Queries = Application.OrderItem.Queries;

namespace WebApi.Controllers
{
    [Route("EasyApi/[controller]")]
    [ApiController]
    public class OrderItemsController : BaseController
    {
        //Queries
        [HttpGet("{orderId}")]
        public async Task<IActionResult> ByOrder([FromRoute] string orderId)
            => Ok(await mediator.Send(new Queries.ItemsByOrder(orderId)));

        //Commands
        [HttpPut]
        public async Task AddItem
            ([FromBody] string orderId, int itemId, int quantity)
            => await mediator.Send(new Commands.AddItem(orderId, itemId, quantity));

        [HttpDelete]
        public async Task RemoveItem([FromBody] int orderItemId)
            => await mediator.Send(new Commands.RemoveItem(orderItemId));
    }
}
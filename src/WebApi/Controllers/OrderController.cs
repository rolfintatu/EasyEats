using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Order.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {
        [HttpPost("Create")]
        public async Task<Unit> CreateOrderforReservation([FromQuery] int reservationId)
            => await mediator.Send(new CreateOrderCommand(reservationId));

        [HttpPost("Add")]
        public async Task<Unit> AddItems([FromQuery] int productId
            , [FromQuery] string orderId
            , [FromQuery] int quantity)
            => await mediator.Send(new AddItemsCommand(productId, orderId, quantity));
    }
}
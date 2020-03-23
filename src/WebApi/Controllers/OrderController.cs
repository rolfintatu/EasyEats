using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Commands = Application.Order.Commands;
using Queires = Application.Order.Queries;
using ValueObjects = Domain.ValueObjects;

namespace WebApi.Controllers
{
    [Route("EasyApi/[controller]")]
    [ApiController]
    public class OrderController : BaseController
    {

        //Commands
        [HttpPut]
        public async Task Create([FromQuery] int reservationId)
            => await mediator.Send(new Commands.CreateOrder(reservationId));

        //Queries
        [HttpGet]
        public async Task<Queires.OrderListResponse> List()
            => await mediator.Send(new Queires.OrderListQuery());

        [HttpGet("Date&UserName")]
        public async Task<Queires.OrderListByUResponse> List
            ([FromBody] DateAndUserName data)
            => await mediator.Send(new Queires.OrderListByUser(
                new ValueObjects.Date(data.Day, data.Month, data.Year), data.UserName));

    }
}
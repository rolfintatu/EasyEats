using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Reservation.Commands.CreateReservation;
using Application.Reservation.Commands.DeleteReservation;
using Application.Reservation.Queries.ReservationsByUser;
using Application.Reservation.Queries.ReservationsList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("EasyApi/[controller]")]
    [ApiController]
    public class ReservationController : BaseController
    {
        //Queries
        [HttpGet]
        public async Task<IActionResult> GetAll()
            => Ok(await mediator.Send(new ReservationsListQuery()));


        [HttpGet("{id}")]
        public async Task<IActionResult> GetByUser([FromRoute] int id)
            => Ok(await mediator.Send(new ResByUserCom()));


        //Commands
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Create([FromBody] CreateReservationCom command)
            => await mediator.Send(command);


        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task Delete([FromBody] int id)
            => await mediator.Send(new CancelReservationCom() { Id = id });

    }
}
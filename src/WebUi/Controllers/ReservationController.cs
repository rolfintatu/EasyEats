using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Reservation.Commands.CreateReservation;
using AppCore.Reservation.Commands.DeleteReservation;
using AppCore.Reservation.Queries.ReservationDetails;
using AppCore.Reservation.Queries.ReservationsByUser;
using AppCore.Reservation.Queries.ReservationsList;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var results = await mediator.Send(new ReservationsListQuery());

            return Ok(results);
        }

        [HttpGet]
        public async Task<ActionResult> GetByUser(int id)
        {
            var reservation = await mediator.Send(new ResByUserCom());

            return Ok(reservation);
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Create([FromBody] CreateReservationCom command)
        {
            await mediator.Send(command);

            return Ok();

        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {

            await mediator.Send(new DeleteReservationCom() { Id = id });

            return Ok();
        }
    }
}
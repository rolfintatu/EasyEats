using System;
using System.Linq;
using System.Threading.Tasks;
using Application.AppReservation.Commands.CancelReservation;
using Application.AppReservation.Commands.CreateReservation;
using Application.AppReservation.Queries.GetReservationDetails;
using Application.AppSchedule.Commands.CreateSchedule;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("EasyApi/[controller]")]
    [ApiController]
    public class ReservationController : BaseController
    {
        ////Queries
        //[HttpGet]
        //public async Task<IActionResult> GetAll()
        //    => Ok(await mediator.Send());


        [HttpGet("{id}")]
        public async Task<IActionResult> ReservationDetails(Guid id)
        {

            if (id == null || id == default(Guid))
                return BadRequest("Please provide a valid id.");

            var reservationDetails = await mediator.Send(
                    new GetReservationDetailsQuery() { ReservationId = id }
                );

            if (reservationDetails is null)
                return NotFound();
            else
                return Ok(reservationDetails);
        }


        //Commands
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateReservationCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest(
                        ModelState.Keys.SelectMany(x => this.ModelState[x].Errors)
                    );

            bool response = false;

            var scheduleResponse = await mediator.Send(
                    new CreateScheduleCommand() { Date = command.Date }
                );

            if (scheduleResponse != Guid.Empty)
            {
                command.ScheduleId = scheduleResponse;
                response = await mediator.Send(command);
            }

            if (response)
                return Ok();
            else
                return BadRequest();
        }


        [HttpPost("reservationId")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cancel(Guid reservationId)
        {
            if (reservationId == null || reservationId == default(Guid))
                return BadRequest("Please provide a valid reservation.");

            var response = await mediator.Send(new CancelReservationCommand(reservationId));

            if (response)
                return Ok();
            else
                return NotFound();
        }

    }
}
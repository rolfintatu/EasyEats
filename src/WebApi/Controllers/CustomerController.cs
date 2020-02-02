using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Customer.Commands.AddAddress;
using Application.Customer.Commands.DeleteAddress;
using Application.Customer.Queries.CustomerDetails;
using Application.Common.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomerController : BaseController
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> AddAddress([FromBody] AddAddressCommand command)
        {
            await mediator.Send(command);

            return NoContent();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAddress()
        {
            await mediator.Send(new DeleteAddressCommand());

            return NoContent();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDetailsDto>> GetDetails([FromQuery]string id)
        {
            var customerDetails = await mediator.Send(new CustomerDetailsQuery() { UserId = id });

            return customerDetails;
        }

    }
}
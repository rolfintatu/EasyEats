using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppCore.Customer.Commands.AddAddress;
using AppCore.Customer.Commands.DeleteAddress;
using AppCore.Customer.Queries.CustomerDetails;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebUi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
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
        public async Task<ActionResult<CustomerDetails>> GetDetails()
        {
            var customerDetails = await mediator.Send(new CustomerDetailsQuery());

            return customerDetails;
        }

    }
}
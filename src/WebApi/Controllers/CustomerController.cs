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

namespace WebApi.Controllers
{
    [Route("EasyApi/[controller]")]
    [ApiController]
    //[Authorize]
    public class CustomerController : BaseController
    {
        //Queires
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Details([FromQuery]string id)
        => Ok(await mediator.Send(new CustomerDetails() { UserId = id }));

        //Commands
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesDefaultResponseType]
        public async Task AddAddress([FromBody] AddAddressCommand command)
        => await mediator.Send(command);


        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task DeleteAddress()
        => await mediator.Send(new DeleteAddressCommand());

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using WebApi.Models;
using MediatR;
using Queries = Application.Customer.Queries;
using Application.Common.Interfaces;
using Application.Common.Dtos;
using System.Net;
using Application.Customer.Commands.CreateCustomer;

namespace WebApi.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {

        [Route("/Token")]
        [HttpGet]
        public async Task<IActionResult> Create(string userName, string password, string grant_type)
        {
             return BadRequest("");
        } 

        [Route("/Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateCustomerModel customerModel)
        {
            return BadRequest("");
        }

        [Route("/ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string code)
        {
            return BadRequest();
        } 
    }
}
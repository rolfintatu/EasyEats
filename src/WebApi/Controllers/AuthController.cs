﻿using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using Application.Auth.Commands.LogInUser;
using Application.Auth.Commands.UserRegistration;

namespace WebApi.Controllers
{
    [ApiController]
    public class AuthController : BaseController
    {

        [Route("/Token")]
        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LogInUserModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Values);

            var result = await mediator.Send(model);

            return Ok(result);
        } 

        [Route("/Register")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] CreateCustomerModel customerModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest();

                await mediator.Send(
                    new UserRegistrationModel(
                        customerModel.Email, customerModel.Password, customerModel.Name, customerModel.Phone
                        ));

                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Route("/ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string code)
        {
            return BadRequest();
        } 
    }
}
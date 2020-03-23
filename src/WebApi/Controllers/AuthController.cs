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
    public class AuthController : Controller
    {
        private readonly AppIdentityDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;
        private readonly IMediator mediator;
        private readonly IEmailService emailService;

        public AuthController(AppIdentityDbContext context,
            UserManager<ApplicationUser> userManager, IMediator mediator
            , IConfiguration configuration, IEmailService emailService)
            => (this.context, this.userManager, this.configuration, this.mediator, this.emailService)
            = (context, userManager, configuration, mediator, emailService);


        [Route("/Token")]
        [HttpGet]
        public async Task<IActionResult> Create(string userName, string password, string grant_type)
        {
            if (await VerifyUserData(userName, password))
            {
                return new ObjectResult(await GenerateToken(userName));
            }
            else
            {
                return BadRequest("");
            }
        } 

        [Route("/Register")]
        [HttpPost]
        public async Task Register([FromBody] CreateCustomerModel customerModel)
        {
            if (await VerifyUserName(customerModel.UserName)
                && await mediator.Send(new Queries.VerifyUser(customerModel.Name)))
            {
                //error
                throw new Exception();
            }
            else
            {
                //register user
                var response = await userManager.CreateAsync(new ApplicationUser(customerModel.UserName), customerModel.Password);

                if (true)
                {
                    // send an email with activation link
                    var user = await userManager.FindByNameAsync(customerModel.UserName);

                    await mediator.Send(new CreateCustomerCommand(user.Id, customerModel.Name, customerModel.Phone));

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var encodeToken = WebUtility.UrlEncode(token);

                    var link = $"https://localhost:5001/ConfirmEmail?userId={ user.Id }&code={ encodeToken }";

                    await emailService.Send(
                        new MessageDto(customerModel.UserName, "Activation Account", link));
                }
            }
        }

        [Route("/ConfirmEmail")]
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail([FromQuery]string userId, [FromQuery]string code)
        {
            var user = await userManager.FindByIdAsync(userId);

            var response = await userManager.ConfirmEmailAsync(user, code);

            if (response.Succeeded)
            {
                return Ok();
            }
            return BadRequest();
        }

        //Private methods
        private async Task<bool> VerifyUserData(string userName, string password)
        {
            var user = await userManager.FindByEmailAsync(userName);
            return await userManager.CheckPasswordAsync(user, password) && await userManager.IsEmailConfirmedAsync(user);
        }

        private async Task<bool> VerifyUserName(string userName)
         => await userManager.FindByEmailAsync(userName) is null ? false : true; 

        private async Task<dynamic> GenerateToken(string userName)
        {
            var user = await userManager.FindByEmailAsync(userName);
            var roles = from ur in context.UserRoles
                        join r in context.Roles on ur.RoleId equals r.Id
                        where ur.UserId == user.Id
                        select new {ur.UserId , ur.RoleId, r.Name};

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            var token = new JwtSecurityToken(
                    new JwtHeader(
                            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("SecretKey"))),
                            SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(claims));

            var output = new
            {
                Access_token = new JwtSecurityTokenHandler().WriteToken(token),
                userName = userName
            };

            return output;

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WebUi.Controllers
{
    public class AuthController : Controller
    {
        private readonly AppIdentityDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration configuration;

        public AuthController(AppIdentityDbContext context, 
            UserManager<ApplicationUser> userManager,
            IConfiguration configuration)
        {
            this.context = context;
            this.userManager = userManager;
            this.configuration = configuration;
        }

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
                return BadRequest();
            }
        } 

        private async Task<bool> VerifyUserData(string userName, string password)
        {
            var user = await userManager.FindByEmailAsync(userName);
            return await userManager.CheckPasswordAsync(user, password);
        }

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
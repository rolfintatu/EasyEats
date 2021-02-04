using Application.Common.Dtos;
using Application.Common.Interfaces;
using Common.Models;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class IdentityService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IEmailService emailService;

        public IdentityService(
            UserManager<ApplicationUser> userManager, 
            IEmailService emailService)
        {
            this.userManager = userManager;
            this.emailService = emailService;
        }

        public async Task<dynamic> GetToken(string userName, string password, string grant_type)
        {
            if (await VerifyUserData(userName, password))
            {
                return await GenerateToken(userName);
            }
            else
            {
                return null;
            }
        }

        public async Task Register(RegistrationModel customerModel)
        {
            if (userManager.FindByEmailAsync(customerModel.Email) != null)
            {
                throw new Exception();
            }
            else
            {
                ApplicationUser user = new ApplicationUser(customerModel.Email);
                var response = await userManager.CreateAsync(user, customerModel.Password);

                if (response.Succeeded)
                {
                    var userData = await userManager.FindByNameAsync(customerModel.Email);

                    var token = await userManager.GenerateEmailConfirmationTokenAsync(userData);
                    var encodeToken = WebUtility.UrlEncode(token);

                    var link = $"https://localhost:5001/ConfirmEmail?userId={ user.Id }&code={ encodeToken }";

                    await emailService.Send(
                        new MessageDto(userData.Email, "Activation Account", link));
                }
            }
        }

        public async Task<bool> ConfirmEmail(string userId, string code)
        {
            var user = await userManager.FindByIdAsync(userId);

            var response = await userManager.ConfirmEmailAsync(user, code);

            return response.Succeeded;
        }


        //Private methods
        private async Task<bool> VerifyUserData(string userName, string password)
        {
            var user = await userManager.FindByEmailAsync(userName);
            return await userManager.CheckPasswordAsync(user, password) && await userManager.IsEmailConfirmedAsync(user);
        }

        private async Task<dynamic> GenerateToken(string userName)
        {
            var user = await userManager.FindByEmailAsync(userName);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString()),
            };

            var token = new JwtSecurityToken(
                    new JwtHeader(
                            new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes("SecretKey")),
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

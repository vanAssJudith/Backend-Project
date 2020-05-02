using Core.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class AuthentiecatieService : IAuthentiecatieService
    {
        private readonly UserManager<Gebruiker> userManager;
        private readonly IConfiguration configuration;
        private readonly ILogger logger;
        private readonly SignInManager<Gebruiker> signInManager;

        public AuthentiecatieService(UserManager<Gebruiker> userManager, IConfiguration configuration, ILogger<AuthenticationService> logger, SignInManager<Gebruiker> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.logger = logger;
            this.signInManager = signInManager;
        }
        public async Task<bool> Login(Login login)
        {
            var result = await signInManager.PasswordSignInAsync(login.UserName, login.Password, false, lockoutOnFailure: false);

            return true;
        }

        public async Task<BearerToken> GenerateJWT(string name)
        {
            var gebruiker = await userManager.FindByNameAsync(name);
            var userClaims = await userManager.GetClaimsAsync(gebruiker);

            if (userClaims.Count == 0)
            {
                var roles = await userManager.GetRolesAsync(gebruiker);

                foreach (var role in roles)
                {
                    var claim = new Claim(ClaimTypes.Role, role);
                    await userManager.AddClaimAsync(gebruiker, claim);
                    userClaims.Add(claim);
                }
            }
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, name));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            //3. Sigin credentials met de symmetric key & encryptie methode
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256); //key en protocol

            //4. aanmaken van het token
            var token = new JwtSecurityToken(
             issuer: configuration["Tokens:Issuer"],  //onze website
             audience: configuration["Tokens:Audience"],//gebruikers
             claims: userClaims,
             expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(configuration["Tokens: Expires"])),
             signingCredentials: creds //controleert token v
             );

            //5. user info returnen (vervaldatum als additionele info)
            return new BearerToken
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token), //token generator
                Expiration = token.ValidTo
            };
        }
    }
}

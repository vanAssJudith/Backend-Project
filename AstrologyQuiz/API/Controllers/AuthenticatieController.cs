using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Models;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticatieController : ControllerBase
    {
        private readonly IAuthentiecatieService authentiecatieService;
        private readonly ILogger<AuthenticatieController> logger;

        public AuthenticatieController(IAuthentiecatieService authentiecatieService, ILogger<AuthenticatieController> logger)
        {
            this.authentiecatieService = authentiecatieService;
            this.logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            try
            {
                if (!await authentiecatieService.Login(login))
                {
                    return BadRequest("Credentials are not correct");
                }
                var token = await authentiecatieService.GenerateJWT(login.UserName);
                return Ok(token);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using PlanNacionalNumeracion.Interfaces;
using PlanNacionalNumeracion.Models;
using PlanNacionalNumeracion.Models.Authentication;

namespace PlanNacionalNumeracion.Controllers
{
    [Route("api/authentication")]
    [ApiController]
    public class AuthenticationController : ControllerBase
	{
        private readonly IAuthentication _authenticationService;
        public AuthenticationController(IAuthentication authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost("login")]
        public IActionResult Auth([FromBody] AuthRequest authRequest)
        {
            var userResponse = _authenticationService.Auth(authRequest);

            if (userResponse == null)
                return BadRequest();

            return Ok(userResponse);
        }

        [HttpGet("validateToken")]
        public IActionResult TokenValidate()
        {
            var _bearer_token = Request.Headers[HeaderNames.Authorization].ToString().Replace("Bearer ", "");

            var userResponse = _authenticationService.Validate(_bearer_token);
            if (userResponse.Equals(null))
            {
                return Unauthorized(new Response { Status = 1, Message = "error de login" });

            }

            return Ok(userResponse);
        }
    }
}


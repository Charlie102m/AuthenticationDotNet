using System;
using System.Threading.Tasks;
using Application.Authentication.Manager;
using Application.Dtos.Authentication;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    /// <summary>
    /// Authentication controller for user register/login
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// For application logging
        /// </summary>
        private readonly ILogger<AuthController> _logger;

        /// <summary>
        /// AutoMapper
        /// </summary>
        private readonly IMapper _mapper;

        /// <summary>
        /// Auth manager
        /// </summary>
        private readonly IAuthManager _manager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logger">For application logging</param>
        /// <param name="mapper">AutoMapper</param>
        /// <param name="manager">Auth manager</param>
        public AuthController(ILogger<AuthController> logger, IMapper mapper, IAuthManager manager) =>
            (_logger, _mapper, _manager) = (logger, mapper, manager);

        /// <summary>
        /// Register endpoint
        /// </summary>
        /// <param name="userForRegister">User submitted details from request body</param>
        /// <returns>Register user or error</returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegister userForRegister)
        {
            LogHttpRequest(HttpContext);

            var user = await _manager.RegisterUserAsync(userForRegister);

            return Ok(user);
        }

        /// <summary>
        /// Login endpoint
        /// </summary>
        /// <param name="userForLogin">User submitted details from request body</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLogin userForLogin)
        {
            LogHttpRequest(HttpContext);

            var token = await _manager.LoginAsync(userForLogin);

            return Ok(new {token});
        }

        private void LogHttpRequest(HttpContext context)
        {
            _logger.LogInformation(
                $"{context.Request.Method} request received: {context.Request.Host}{context.Request.Path} - {DateTime.Now.ToLocalTime()}");
        }
    }
}

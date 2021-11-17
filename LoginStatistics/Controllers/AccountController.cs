using LoginStatistics.Application.Features.Users.Commands.CreateUser;
using LoginStatistics.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginStatistics.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            return Ok(await Mediator.Send(command));
        }
        [HttpPost("authenticate")]
        public IActionResult AuthenticateAsync(string email)
        {
            return Ok(_accountService.AuthenticateAsync(email));
        }
        [HttpPost("revokeByRefreshToken")]
        public IActionResult RevokeByRefreshToken([FromQuery] string refreshToken)
        {
            return Ok(_accountService.RevokeByRefreshToken(refreshToken));
        }
    }
}

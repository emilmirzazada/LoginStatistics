using LoginStatistics.Application.Features.LoginAttempts.Commands.DeleteAllLoginAttempts;
using LoginStatistics.Application.Features.RandomData.Commands.GenerateRandomName;
using LoginStatistics.Application.Features.Users.Commands.CreateUser;
using LoginStatistics.Application.Features.Users.Commands.DeleteAllUsers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginStatistics.API.Controllers
{
    public class InitializeController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await Mediator.Send(new DeleteAllUsersCommand());
            await Mediator.Send(new DeleteAllLoginAttemptsCommand());

            for (int i = 0; i < 20; i++)
            {
                await Mediator.Send(new CreateUserCommand
                {
                    Name = await Mediator.Send(new GenerateRandomNameCommand { Length = 4 }),
                    Surname = await Mediator.Send(new GenerateRandomNameCommand { Length = 6 })
                });
            }
            return Ok();
        }
    }
}

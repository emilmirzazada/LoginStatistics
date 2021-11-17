using LoginStatistics.Application.Features.LoginAttempts.Commands.CreateLoginAttempt;
using LoginStatistics.Application.Features.LoginAttempts.Commands.DeleteAllLoginAttempts;
using LoginStatistics.Application.Features.RandomData.Commands.GenerateRandomDate;
using LoginStatistics.Application.Features.RandomData.Commands.GenerateRandomName;
using LoginStatistics.Application.Features.Users.Commands.CreateUser;
using LoginStatistics.Application.Features.Users.Commands.DeleteAllUsers;
using LoginStatistics.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginStatistics.API.Controllers
{
    [Authorize]
    public class InitializeController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            await Mediator.Send(new DeleteAllUsersCommand());
            await Mediator.Send(new DeleteAllLoginAttemptsCommand());

            for (int i = 0; i < 50; i++)
            {
                string name = await Mediator.Send(new GenerateRandomNameCommand { Length = 4 });
                string surname = await Mediator.Send(new GenerateRandomNameCommand { Length = 6 });
                User user = await Mediator.Send(new CreateUserCommand
                {
                    Name = name,
                    Surname = surname,
                    Email = name + surname + "@GMAIL.COM"
                });

                for (int j = 0; j < 4; j++)
                {
                    await Mediator.Send(new CreateLoginAttemptCommand
                    {
                        UserId = user.Id,
                        AttemptTime = await Mediator.Send(new GenerateRandomDateCommand
                        {
                            StartDate = new DateTime(2017, 10, 26),
                            EndDate = new DateTime(2021, 11, 20)
                        }),
                        IsSuccess = j % 2 == 0 ? true : false
                    });
                }
            }
            return Ok();
        }
    }
}

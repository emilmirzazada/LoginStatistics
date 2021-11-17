using LoginStatistics.Application.Features.Users.Queries.GetAllUsers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginStatistics.API.Controllers
{
    public class UserController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> Get(int pageNumber, int pageSize)
        {
            return Ok(await Mediator.Send(new GetAllUsersQuery {PageNumber=pageNumber,PageSize=pageSize }));
        }
    }
}

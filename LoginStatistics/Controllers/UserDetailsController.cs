using LoginStatistics.Application.Features.UserDetails.Queries.GetUserDetails;
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
    public class UserDetailsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetByEmail(string email)
        {
            return Ok(await Mediator.Send(new GetUserDetailsQuery { Email = email }));
        }
    }
}

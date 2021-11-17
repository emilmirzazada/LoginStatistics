using LoginStatistics.Application.Features.LoginAttempts.Queries.GetLoginAttemptsCounters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginStatistics.API.Controllers
{
    public class StatisticsController : BaseApiController
    {
        [HttpPost]
        public async Task<IActionResult> GetLoginAttemptsCounters([FromBody] GetLoginAttemptsCountersQuery query)
        {
            return Ok(await Mediator.Send(query));
        }
    }
}

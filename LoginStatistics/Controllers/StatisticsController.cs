using LoginStatistics.Application.Features.LoginAttempts.Queries.GetLoginAttemptsCounters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginStatistics.API.Controllers
{
    [Authorize]
    public class StatisticsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetLoginAttemptsCounters(string startDate, string endDate, string metric,
            bool isSuccess)
        {
            return Ok(await Mediator.Send(new GetLoginAttemptsCountersQuery
            {
                StartDate=startDate,
                EndDate=endDate,
                Metric=metric,
                IsSuccess=isSuccess
            }));
        }
    }
}

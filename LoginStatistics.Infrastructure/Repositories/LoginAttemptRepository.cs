using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using LoginStatistics.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace LoginStatistics.Infrastructure.Repositories
{
    public class LoginAttemptRepository : GenericRepository<UserLoginAttempt>, ILoginAttemptRepository
    {
        private readonly DbSet<UserLoginAttempt> userLoginAttempts;
        private readonly ApplicationContext _ctx;

        public LoginAttemptRepository(ApplicationContext dbContext) : base(dbContext)
        {
            userLoginAttempts = dbContext.Set<UserLoginAttempt>();
            _ctx = dbContext;
        }

        public IEnumerable<object> GetLoginAttemptsCounters(string startDate, string endDate, string metric,
                                                              bool isSuccess)
        {
            IEnumerable<UserLoginAttempt> loginAttempts;
            if (startDate == null && endDate == null)
                loginAttempts = _ctx.LoginAttempts;
            else if (startDate != null && endDate == null)
                loginAttempts = _ctx.LoginAttempts
                                .Where(x => x.AttemptTime >= DateTime.Parse(startDate));
            else if (startDate == null && endDate != null)
                loginAttempts = _ctx.LoginAttempts
                                .Where(x => x.AttemptTime <= DateTime.Parse(endDate));
            else
                loginAttempts = _ctx.LoginAttempts
                                .Where(x => x.AttemptTime >= DateTime.Parse(startDate)
                                && x.AttemptTime <= DateTime.Parse(endDate));

            loginAttempts = loginAttempts
                                .Where(l => l.IsSuccess == isSuccess);


            if (metric == "hour")
            {
                   var statisticsData =
                (from d in loginAttempts
                 group d by new
                 {
                     Year = d.AttemptTime.Year,
                     Month = d.AttemptTime.Month,
                     Day = d.AttemptTime.Day,
                     Hour = d.AttemptTime.Hour
                 } into g
                 select new
                 {
                     Year = g.Key.Year,
                     Month = g.Key.Month,
                     Day = g.Key.Day,
                     Hour = g.Key.Hour,
                     Value = g.Count()
                 }
               ).AsEnumerable()
                .Select(g => new
                {
                    Period = g.Year + "-" + g.Month + "-" + g.Day + " " + g.Hour + ":00",
                    Value = g.Value
                });

                return statisticsData;
            }
            else if (metric == "month")
            {
                var statisticsData =
            (from d in loginAttempts
             group d by new
             {
                 Year = d.AttemptTime.Year,
                 Month = d.AttemptTime.Month
             } into g
             select new
             {
                 Year = g.Key.Year,
                 Month = g.Key.Month,
                 Value = g.Count()
             }
           ).AsEnumerable()
            .Select(g => new
            {
                Period = new DateTime(g.Year, g.Month, 1).ToString("MMMM", CultureInfo.InvariantCulture)+", " + g.Year,
                Value = g.Value
            });

                return statisticsData;
            }
            else
            {
                var statisticsData =
                (from d in loginAttempts
                 group d by new
                 {
                     Year = d.AttemptTime.Year
                 } into g
                 select new
                 {
                     Year = g.Key.Year,
                     Value = g.Count()
                 }
               ).AsEnumerable()
                .Select(g => new
                {
                    Period = g.Year,
                    Value = g.Value
                });

                return statisticsData;
            }

        }
    }
}

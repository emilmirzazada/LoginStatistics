using LoginStatistics.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Infrastructure.Services
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.UtcNow.AddHours(2);
    }
}

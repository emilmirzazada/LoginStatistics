using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Application.Interfaces
{
    public interface IDateTimeService
    {
        DateTime Now { get; }
    }
}

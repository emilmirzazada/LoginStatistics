using LoginStatistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Application.Interfaces.Repositories
{
    public interface ILoginAttemptRepository : IGenericRepository<UserLoginAttempt>
    {

    }
}

using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using LoginStatistics.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Infrastructure.Repositories
{
    public class LoginAttemptRepository : GenericRepository<UserLoginAttempt>, ILoginAttemptRepository
    {
        private readonly DbSet<UserLoginAttempt> userLoginAttempts;

        public LoginAttemptRepository(ApplicationContext dbContext) : base(dbContext)
        {
            userLoginAttempts = dbContext.Set<UserLoginAttempt>();
        }
    }
}

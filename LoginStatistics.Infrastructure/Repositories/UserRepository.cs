using LoginStatistics.Application.Interfaces;
using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using LoginStatistics.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> users;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            users = dbContext.Set<User>();
        }
    }
}

using LoginStatistics.Application.Interfaces;
using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using LoginStatistics.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LoginStatistics.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly DbSet<User> users;
        private readonly ApplicationContext _ctx;

        public UserRepository(ApplicationContext dbContext) : base(dbContext)
        {
            users = dbContext.Set<User>();
            _ctx = dbContext;
        }

        public IEnumerable<User> GetUserDetailsByEmail(string email)
        {
            IEnumerable<User> users = _ctx.Users
                                        .Where(u => u.Email == email)
                                        .Include(x => x.LoginAttempts);
            return users;
        }
    }
}

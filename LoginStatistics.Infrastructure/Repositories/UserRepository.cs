using LoginStatistics.Application.Interfaces;
using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using LoginStatistics.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<User>> GetAllUsers(int pageNumber, int pageSize)
        {

            var users = await _ctx
                .Set<User>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Include(x=>x.LoginAttempts)
                .AsNoTracking()
                .ToListAsync();
            return users;
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

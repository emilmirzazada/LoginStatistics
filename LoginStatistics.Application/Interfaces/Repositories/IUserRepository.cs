using LoginStatistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<IEnumerable<User>> GetAllUsers(int pageNumber, int pageSize);
        IEnumerable<User> GetUserDetailsByEmail(string email);
    }
}

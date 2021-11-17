﻿using LoginStatistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Application.Interfaces.Repositories
{
    public interface IUserRepository : IGenericRepository<User>
    {
        IEnumerable<User> GetUserDetailsByEmail(string email);
    }
}

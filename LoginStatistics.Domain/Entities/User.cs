using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public List<UserLoginAttempt> LoginAttempts { get; set; }

        public int? RefreshTokenId { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }
}

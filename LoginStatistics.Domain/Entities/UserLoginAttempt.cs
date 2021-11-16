using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Domain.Entities
{
    public class UserLoginAttempt
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime AttemptTime { get; set; }
        public bool IsSuccess { get; set; }
    }
}

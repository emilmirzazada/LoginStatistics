using AutoMapper;
using LoginStatistics.Application.Features.LoginAttempts.Commands.CreateLoginAttempt;
using LoginStatistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Application.Mappings
{
    public class LoginAttemptProfile : Profile
    {
        public LoginAttemptProfile()
        {
            CreateMap<CreateLoginAttemptCommand, UserLoginAttempt>().ConstructUsing(c => new UserLoginAttempt()
            {
                Id = Guid.NewGuid()
            });

        }
    }
}

using AutoMapper;
using LoginStatistics.Application.Features.Users.Commands.CreateUser;
using LoginStatistics.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserCommand, User>().ConstructUsing(c => new User()
            {
                Id = Guid.NewGuid()
            });

        }
    }
}

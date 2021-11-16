using AutoMapper;
using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Features.Users.Commands.CreateUser
{
    public partial class CreateUserCommand : IRequest<User>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public CreateUserCommandHandler(IUserRepository UserRepository, IMapper mapper)
        {
            _userRepository = UserRepository;
            _mapper = mapper;
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {

            var User = _mapper.Map<User>(request);
            await _userRepository.AddAsync(User);
            return new User();

        }
    }
}

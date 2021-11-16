using AutoMapper;
using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Features.LoginAttempts.Commands.CreateLoginAttempt
{
    public class CreateLoginAttemptCommand : IRequest<UserLoginAttempt>
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
    public class CreateLoginAttemptCommandHandler : IRequestHandler<CreateLoginAttemptCommand, UserLoginAttempt>
    {
        private readonly ILoginAttemptRepository _LoginAttemptRepository;
        private readonly IMapper _mapper;
        public CreateLoginAttemptCommandHandler(ILoginAttemptRepository LoginAttemptRepository, IMapper mapper)
        {
            _LoginAttemptRepository = LoginAttemptRepository;
            _mapper = mapper;
        }

        public async Task<UserLoginAttempt> Handle(CreateLoginAttemptCommand request, CancellationToken cancellationToken)
        {

            var LoginAttempt = _mapper.Map<UserLoginAttempt>(request);
            await _LoginAttemptRepository.AddAsync(LoginAttempt);
            return LoginAttempt;

        }

    }
}

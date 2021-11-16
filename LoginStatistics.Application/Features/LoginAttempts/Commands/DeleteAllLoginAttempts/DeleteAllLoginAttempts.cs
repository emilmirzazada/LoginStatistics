using LoginStatistics.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Features.LoginAttempts.Commands.DeleteAllLoginAttempts
{
    public class DeleteAllLoginAttemptsCommand : IRequest<bool>
    {
        public class DeleteAllLoginAttemptsCommandHandler : IRequestHandler<DeleteAllLoginAttemptsCommand, bool>
        {
            private readonly ILoginAttemptRepository _loginAttemptRepository;
            public DeleteAllLoginAttemptsCommandHandler(ILoginAttemptRepository loginAttemptRepository)
            {
                _loginAttemptRepository = loginAttemptRepository;
            }
            public async Task<bool> Handle(DeleteAllLoginAttemptsCommand command, CancellationToken cancellationToken)
            {
                await _loginAttemptRepository.DeleteAll("LoginAttempts");
                return true;
            }
        }
    }
}

using LoginStatistics.Application.Interfaces.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Features.Users.Commands.DeleteAllUsers
{
    public class DeleteAllUsersCommand : IRequest<bool>
    {
        public class DeleteAllUsersCommandHandler : IRequestHandler<DeleteAllUsersCommand, bool>
        {
            private readonly IUserRepository _userRepository;
            public DeleteAllUsersCommandHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<bool> Handle(DeleteAllUsersCommand command, CancellationToken cancellationToken)
            {
                await _userRepository.DeleteAll("Users");
                return true;
            }
        }
    }
}

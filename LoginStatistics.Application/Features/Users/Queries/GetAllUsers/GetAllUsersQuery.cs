using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Features.Users.Queries.GetAllUsers
{
    public class GetAllUsersQuery : IRequest<IEnumerable<User>>
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, IEnumerable<User>>
        {
            private readonly IUserRepository _userRepository;
            public GetAllUsersQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<IEnumerable<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
            {
                var users = await _userRepository.GetAllUsers(request.PageNumber,request.PageSize);
                return users;
            }

        }
    }
}

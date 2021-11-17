using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Features.UserDetails.Queries.GetUserDetails
{
    public class GetUserDetailsQuery : IRequest<IEnumerable<User>>
    {
        public string Email { get; set; }
        public class GetUserDetailsQueryHandler : IRequestHandler<GetUserDetailsQuery, IEnumerable<User>>
        {
            private readonly IUserRepository _userRepository;
            public GetUserDetailsQueryHandler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }

            public async Task<IEnumerable<User>> Handle(GetUserDetailsQuery request, CancellationToken cancellationToken)
            {
                var users = _userRepository.GetUserDetailsByEmail(request.Email);
                return await Task.FromResult(users);
            }

        }
    }
}

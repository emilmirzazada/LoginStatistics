using AutoMapper;
using LoginStatistics.Application.Interfaces.Repositories;
using LoginStatistics.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Features.LoginAttempts.Queries.GetLoginAttemptsCounters
{
    public class GetLoginAttemptsCountersQuery : IRequest<IEnumerable<object>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Metric { get; set; }
        public bool IsSuccess { get; set; }
        public class GetLoginAttemptsQueryHandler : IRequestHandler<GetLoginAttemptsCountersQuery, IEnumerable<object>>
        {
            private readonly ILoginAttemptRepository _loginAttemptRepository;
            private readonly IMapper _mapper;

            public GetLoginAttemptsQueryHandler(ILoginAttemptRepository loginAttemptRepository, IMapper mapper)
            {
                _loginAttemptRepository = loginAttemptRepository;
                _mapper = mapper;
            }

            public async Task<IEnumerable<object>> Handle(GetLoginAttemptsCountersQuery request, CancellationToken cancellationToken)
            {
                var loginAttemptsCounters = _loginAttemptRepository.GetLoginAttemptsCounters
                    (request.StartDate,request.EndDate,request.Metric, request.IsSuccess);

                return await Task.FromResult(loginAttemptsCounters);
            }

        }
    }
}

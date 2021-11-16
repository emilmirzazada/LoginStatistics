using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Features.RandomData.Commands.GenerateRandomDate
{
    public class GenerateRandomDateCommand : IRequest<DateTime>
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class GenerateRandomDateCommandHandler : IRequestHandler<GenerateRandomDateCommand, DateTime>
    {

        public async Task<DateTime> Handle(GenerateRandomDateCommand request, CancellationToken cancellationToken)
        {
            var randomTest = new Random();

            TimeSpan timeSpan = request.EndDate - request.StartDate;
            TimeSpan newSpan = new TimeSpan(0, randomTest.Next(0, (int)timeSpan.TotalMinutes), 0);
            DateTime newDate = request.StartDate + newSpan;

            return await Task.FromResult(newDate);

        }
    }
}

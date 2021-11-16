using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Features.RandomData.Commands.GenerateRandomName
{
    public class GenerateRandomNameCommand : IRequest<string>
    {
        public int Length { get; set; }
    }
    public class GenerateRandomNameCommandHandler : IRequestHandler<GenerateRandomNameCommand, string>
    {

        public async Task<string> Handle(GenerateRandomNameCommand request, CancellationToken cancellationToken)
        {
            Random r = new Random();
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "y" };
            StringBuilder Name = new StringBuilder("");
            Name.Append(consonants[r.Next(consonants.Length)].ToUpper());
            Name.Append(vowels[r.Next(vowels.Length)]);
            int b = 2;
            while (b < request.Length)
            {
                Name.Append(consonants[r.Next(consonants.Length)]);
                b++;
                Name.Append(vowels[r.Next(vowels.Length)]);
                b++;
            }

            return await Task.FromResult(Name.ToString());

        }
    }
}

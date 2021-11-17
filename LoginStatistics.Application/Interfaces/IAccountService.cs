using LoginStatistics.Application.DTOs;
using LoginStatistics.Application.DTOs.Jwt;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LoginStatistics.Application.Interfaces
{
    public interface IAccountService
    {
        AuthenticationResponse AuthenticateAsync(string email);
        JwtTokenDto RevokeByRefreshToken(string token);
    }
}

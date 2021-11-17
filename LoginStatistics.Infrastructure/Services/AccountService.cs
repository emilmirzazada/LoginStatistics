using LoginStatistics.Application.DTOs;
using LoginStatistics.Application.DTOs.Jwt;
using LoginStatistics.Application.Interfaces;
using LoginStatistics.Domain.Entities;
using LoginStatistics.Domain.Settings;
using LoginStatistics.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginStatistics.Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationContext context;
        private readonly JWTSettings _jwtSettings;
        private readonly IDateTimeService _dateTimeService;
        public AccountService(
            IOptions<JWTSettings> jwtSettings,
            IDateTimeService dateTimeService,
            ApplicationContext context)
        {
            _jwtSettings = jwtSettings.Value;
            _dateTimeService = dateTimeService;
            this.context = context;
        }

        public AuthenticationResponse AuthenticateAsync(string email)
        {
            var user = context.Users.
                        Include(x => x.RefreshToken)
                        .Where(x => x.Email == email)
                        !.FirstOrDefault();

            if (user == null)
            {
                throw new Exception($"No Accounts Registered with {email}.");
            }

            var attempt = new UserLoginAttempt
            {
                UserId = user.Id,
                AttemptTime = _dateTimeService.Now,
                IsSuccess = true
            };
            context.LoginAttempts.Add(attempt);
            context.SaveChanges();

            JwtTokenDto jwtTokenDto = GenerateJWToken(user);
            AuthenticationResponse response = new AuthenticationResponse();
            response.Id = user.Id;
            response.Jwt = jwtTokenDto;
            response.Email = user.Email;
            response.UserName = user.Name;


            if (user.RefreshToken == null || user.RefreshToken.IsExpired)
            {
                var refreshToken = GenerateRefreshToken();
                user.RefreshToken = refreshToken;
                context.Users.Update(user);
                context.SaveChanges();
                response.RefreshToken = new RefreshTokenDto(refreshToken.Token, refreshToken.Expires);
            }
            else
            {
                response.RefreshToken = new RefreshTokenDto(user.RefreshToken.Token, user.RefreshToken.Expires);
            }

            return response;
        }

        private JwtTokenDto GenerateJWToken(User user)
        {

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("Id",user.Id.ToString())
            };


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var expires = _dateTimeService.Now.AddMinutes(_jwtSettings.DurationInMinutes);
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: signingCredentials);
            var tokentHandler = new JwtSecurityTokenHandler();
            string token = tokentHandler.WriteToken(jwtSecurityToken);
            return new JwtTokenDto(token, expires);
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = _dateTimeService.Now.AddMonths(6),
                Created = _dateTimeService.Now
            };
        }

        public JwtTokenDto RevokeByRefreshToken(string token)
        {
            var refreshToken = context.RefreshTokens.Where(x => x.Token == token)!.FirstOrDefault();
            User user=null;
            if(refreshToken!=null)
                user = context.Users.Where(x => x.RefreshTokenId == refreshToken.Id)!.FirstOrDefault();
            if (user != null)
            {

                JwtTokenDto jwtTokenDto = GenerateJWToken(user);
                return new JwtTokenDto(jwtTokenDto.Token, jwtTokenDto.Expires);
            }
            return new JwtTokenDto("There is no account related with this token", DateTime.Now);
        }
    }
}

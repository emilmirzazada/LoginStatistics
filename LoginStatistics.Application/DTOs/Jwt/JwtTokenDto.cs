using System;
using System.Collections.Generic;
using System.Text;

namespace LoginStatistics.Application.DTOs.Jwt
{
    public class JwtTokenDto
    {
        public string Token { get; set; }
        public DateTimeOffset Expires { get; set; }

        public JwtTokenDto(string token, DateTimeOffset expires)
        {
            Token = token;
            Expires = expires;
        }
    }
}

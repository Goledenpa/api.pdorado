using api.pdorado.Servicios.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api.pdorado.Auth
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public JWTMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _configuration = configuration;
        }

        public async Task Invoke(HttpContext context, IUsuarioService usuarioService)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                attachAccountToContext(context, token, usuarioService);
            }

            await _next(context);
        }

        private void attachAccountToContext(HttpContext context, string token, IUsuarioService usuarioService)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));
                tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _configuration.GetValue<string>("Jwt:Issuer"),
                    ValidAudience = _configuration.GetValue<string>("Jwt:Audience")
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = jwtToken.Claims.First(x => x.Type == "id").Value;

                context.Items["User"] = usuarioService.GetUsuario(accountId);
            }
            catch { }
        }
    }
}

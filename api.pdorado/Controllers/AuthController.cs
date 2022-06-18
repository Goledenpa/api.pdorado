using Microsoft.AspNetCore.Mvc;
using api.pdorado.Data;
using Microsoft.AspNetCore.Authorization;
using pdorado.data.Models;
using api.pdorado.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using api.pdorado.Servicios.Interfaces;

namespace api.pdorado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IUsuarioService _usuarioService;

        public AuthController(DataContext context, IConfiguration configuration, IUsuarioService usuarioService)
        {
            _context = context;
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        [AllowAnonymous]
        [HttpPost("")]
        public IActionResult Auth([FromBody] UsuarioDTO user)
        {
            bool isValid = _usuarioService.Login(user);

            if (isValid)
            {
                var tokenString = GenerateJwtToken(user.Login);
                return Ok(new { Token = tokenString, Message = "Success" });
            }

            return BadRequest("Introduzca un nombre de usuario y contraseña válidos");
        }

        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(IsAuthenticated))]
        public IActionResult IsAuthenticated()
        {
            return Ok("El usuario está autenticado");
        }


        private string GenerateJwtToken(string login)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("Jwt:Key"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", login) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration.GetValue<string>("Jwt:Issuer"),
                Audience = _configuration.GetValue<string>("Jwt:Audience"),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor); 
            return tokenHandler.WriteToken(token);
        }
    }
}
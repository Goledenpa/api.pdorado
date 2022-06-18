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
    /// <summary>
    /// Controlador de la Autorización
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        /// <summary>
        /// Interfaz que permite obtener datos del appsettings.json
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// Servicio que permite la autentificación de los usuarios
        /// </summary>
        private readonly IUsuarioService _usuarioService;

        public AuthController(IConfiguration configuration, IUsuarioService usuarioService)
        {
            _configuration = configuration;
            _usuarioService = usuarioService;
        }

        /// <summary>
        /// Método que permite que un usuario se autentifique
        /// </summary>
        /// <param name="user">Usuario que intenta autentificarse</param>
        /// <returns>Un DTO de usuario si se ha podido autentificar, un error 400 si no</returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Auth([FromBody] UsuarioDTO user)
        {
            bool isValid = await _usuarioService.Login(user);

            if (isValid)
            {
                var tokenString = GenerateJwtToken(user.Login);
                UsuarioDTO dto = await _usuarioService.GetUsuario(user.Login);
                return Ok(new 
                {
                    dto.Login,
                    dto.Email,
                    dto.Apellidos,
                    dto.Nombre,
                    dto.FechaNacimiento,
                    dto.IdLenguaje,
                    tokenString
                });
            }

            return BadRequest("Introduzca un nombre de usuario y contraseña válidos");
        }

        /// <summary>
        /// Método para saber si el usuario está autentificado
        /// </summary>
        /// <returns></returns>
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(IsAuthenticated))]
        public IActionResult IsAuthenticated()
        {
            return Ok("El usuario está autenticado");
        }

        /// <summary>
        /// Generación de un Bearer JWT
        /// </summary>
        /// <param name="login">Login del usuario</param>
        /// <returns>El token jwt que se ha generado</returns>
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
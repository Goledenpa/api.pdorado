using api.pdorado.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using pdorado.data.Models;
using System;

namespace api.pdorado.Auth
{
    /// <summary>
    /// Atributo que permite la autorización para usar los métodos del controller
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        /// <summary>
        /// Parámetro que permite que se ejecute el método solo si el usuario que lo pide es administrador
        /// </summary>
        public bool Admin { get; set; }

        /// <summary>
        /// Al llamar al Atributo para por este método que devuelve un error 401 si el usuario no está autorizado a ejecutar el método
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            UsuarioDTO account = (UsuarioDTO)context.HttpContext.Items["User"];

            if (account == null || (Admin && !account.IsAdmin))
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}

using api.pdorado.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using pdorado.data.Models;
using System;

namespace api.pdorado.Auth
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public bool Admin { get; set; }
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

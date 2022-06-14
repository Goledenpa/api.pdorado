using Microsoft.AspNetCore.Mvc;
using api.pdorado.Data;

namespace api.pdorado.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

    }
}
using Empresa.Models;
using Empresa.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Controllers
{
    [Route("api/Autenticacao")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly TokenGenerator _tokenGenerator;

        public AuthController(TokenGenerator tokenGenerator)
        {
            _tokenGenerator = tokenGenerator;
        }
        /// <summary>
        /// Autenticacao com Email e Password.
        /// </summary>
        [HttpPost("Login")]
        public ActionResult Login(User user)
        { 
            if(user.Password == "10082007Pereira@" && user.Email == "raian@gmail.com")
            {
                var token = _tokenGenerator.GerarToken(user);
                var refreshToken = _tokenGenerator.GenerateRefreshToken();

                return Ok(new { token, refreshToken });
            }
            return NotFound();
        }

        /// <summary>
        /// Listagem de Itens.
        /// </summary>
        [Authorize]
        [HttpGet("Listar")]
        public ActionResult Listar()
        {
            return Ok(new 
            {
                Valido = true,
            });
        }
    }
}

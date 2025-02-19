using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Empresa.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcessController : ControllerBase
    {
        /// <summary>
        /// Listagem de Itens.
        /// </summary>

        [Authorize]
        [HttpGet("Validar-Autorizacao")]
        public ActionResult Listar()
        {
            return Ok(new
            {
                Autenticado = "Sucesso"
            });
        }
    }
}

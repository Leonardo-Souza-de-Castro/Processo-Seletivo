using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_Cadastro.Interfaces;
using System;

namespace Projeto_Cadastro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoController : ControllerBase
    {
        private readonly ITipoRepository _context;

        public TipoController(ITipoRepository context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo responsavel por listar todos os tipos de usuarios existentes
        /// </summary>
        /// <returns>Uma lista de tipos de ususarios</returns>
        [Authorize(Roles = "2,3")]
        [HttpGet]
        public IActionResult Listar_Tipos()
        {
            try
            {
                return Ok(_context.Listar());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}

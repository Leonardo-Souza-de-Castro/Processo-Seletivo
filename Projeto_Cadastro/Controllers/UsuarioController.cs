using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projeto_Cadastro.Interfaces;
using Projeto_Cadastro.Utils;
using Projeto_Cadastro.ViewModels;
using System;

namespace Projeto_Cadastro.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _context;

        public UsuarioController(IUsuarioRepository context)
        {
            _context = context;
        }

        /// <summary>
        /// Metodo resposavel pelo cadastro de novos usuarios no sistema
        /// </summary>
        /// <param name="usuarionovo">Novo usuario</param>
        /// <returns></returns>
        [Authorize(Roles = "2,3")]
        [HttpPost]
        public IActionResult Cadastrar(usuarioViewModel usuarionovo)
        {
            try
            {
                _context.Cadastrar(usuarionovo);

                return StatusCode(201);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Metodo responsavel por listar as informações especificas de um usuario
        /// </summary>
        /// <param name="id">id do usuario a ser buscado</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id}")]
        public IActionResult Buscar_Id(int id)
        {
            try
            {
                return Ok(_context.Buscar(id));
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Metodo responsavel por listar todos os usuario
        /// </summary>
        /// <returns>Uma lista com todos os usuarios</returns>
        [Authorize(Roles = "2,3")]
        [HttpGet]
        public IActionResult Listar_Todos()
        {
            try
            {
                return Ok(_context.Listar_Todos());
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Metodo resposanvel por deletar um usuario cadastrado
        /// </summary>
        /// <param name="id">Id do usuario a ser apagado</param>
        [Authorize(Roles = "3")]
        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            try
            {
                _context.Deletar(id);

                return StatusCode(203);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }

        /// <summary>
        /// Metodo responsavel por atualizar as informações de um usuario
        /// </summary>
        /// <param name="usuario_Atualizado">Novas informações</param>
        /// <param name="id">id do usuario a ser atualizado</param>
        /// <returns></returns>
        [Authorize(Roles = "2,3")]
        [HttpPatch("{id}")]
        public IActionResult Atualizar(usuarioViewModel usuario_Atualizado, int id)
        {
            try
            {
                _context.Editar(usuario_Atualizado, id);

                return StatusCode(204);
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}

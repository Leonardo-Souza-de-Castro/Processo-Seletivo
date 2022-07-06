using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projeto_Cadastro.Domains;
using Projeto_Cadastro.Interfaces;
using Projeto_Cadastro.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Projeto_Cadastro.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public LoginController(IUsuarioRepository context)
        {
            _usuarioRepository = context;
        }

        /// <summary>
        /// Metodo responsavel por fazer login no sistema
        /// </summary>
        /// <param name="login">Objeto do tipo Login com Email e Senha</param>
        /// <returns>Usuario com esse email e senha</returns>
        [HttpPost]
        public IActionResult Login(loginViewModel login)
        {
            try
            {
                Usuario UsuarioBuscado = _usuarioRepository.Login(login.Email, login.Senha);

                if (UsuarioBuscado != null)
                {
                    var MinhasClains = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Email, UsuarioBuscado.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, UsuarioBuscado.IdUsuario.ToString()),
                        new Claim(ClaimTypes.Role, UsuarioBuscado.IdTipo.ToString()),
                        new Claim ("role", UsuarioBuscado.IdTipo.ToString())
                    };

                    var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("cadastrousuarios-chave-autenticacao"));

                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var meuToken = new JwtSecurityToken(
                        issuer: "Projeto_Cadastro_webAPI",
                        audience: "Projeto_Cadastro_webAPI",
                        claims: MinhasClains,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: creds
                        );

                    return Created("uri", new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(meuToken)
                    });
                }

                return BadRequest("Email ou Senha Invalidos!");
            }
            catch (Exception ex)
            {

                return BadRequest(ex);
            }
        }
    }
}

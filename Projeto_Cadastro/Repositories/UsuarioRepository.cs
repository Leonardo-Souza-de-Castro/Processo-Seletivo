using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projeto_Cadastro.Context;
using Projeto_Cadastro.Domains;
using Projeto_Cadastro.Interfaces;
using Projeto_Cadastro.Utils;
using Projeto_Cadastro.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_Cadastro.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly CadastroContext ctx;

        public UsuarioRepository(CadastroContext appContext)
        {
            ctx = appContext;
        }

        public Usuario Buscar(int id)
        {
            return ctx.Usuarios.Include(x => x.IdTipoNavigation).Select(x => new Usuario()
            {
                IdUsuario = x.IdUsuario,
                Imagem = x.Imagem,
                Nome = x.Nome,
                Email = x.Email,
                StatusConta = x.StatusConta,
                IdTipoNavigation = new TipoUsuario()
                {
                    NomeTipo = x.IdTipoNavigation.NomeTipo
                }
            }).FirstOrDefault(x => x.IdUsuario == id);
        }

        public void Cadastrar([FromForm] usuarioViewModel Usuario_Novo)
        {
            Usuario Usuario_novo = new Usuario();

            Usuario_novo.Nome = Usuario_Novo.Nome;
            Usuario_novo.Senha = Crypto.Gerar_Hash(Usuario_Novo.Senha);
            Usuario_novo.Email = Usuario_Novo.Email;
            Usuario_novo.StatusConta = Usuario_Novo.StatusConta;
            Usuario_novo.Imagem = Usuario_Novo.Imagem;
            Usuario_novo.IdTipo = Usuario_Novo.IdTipo;

            ctx.Usuarios.Add(Usuario_novo);

            ctx.SaveChanges();
        }

        public void Deletar(int id)
        {
            Usuario Usuario_Removido = Buscar(id);

            ctx.Usuarios.Remove(Usuario_Removido);

            ctx.SaveChanges();
        }

        public void Editar([FromForm] usuarioViewModel Usuario_Atualizado, int id)
        {
            Usuario Usuario_novo = ctx.Usuarios.FirstOrDefault(x => x.IdUsuario == id);

            if (Usuario_Atualizado.Nome != null)
            {
            Usuario_novo.Nome = Usuario_Atualizado.Nome;
            }

            if (Usuario_Atualizado.Senha != null)
            {
            Usuario_novo.Senha = Usuario_Atualizado.Senha;
            }

            if (Usuario_Atualizado.Email != null)
            {
            Usuario_novo.Email = Usuario_Atualizado.Email;
            }

            if (Usuario_Atualizado.StatusConta != null)
            {
            Usuario_novo.StatusConta = Usuario_Atualizado.StatusConta;
            }
            
            if (Usuario_Atualizado.Imagem != null)
            {
            Usuario_novo.Imagem = Usuario_Atualizado.Imagem;
            }
            
            if (Usuario_Atualizado.IdTipo != null)
            {
            Usuario_novo.IdTipo = Usuario_Atualizado.IdTipo;
            }

            ctx.Usuarios.Update(Usuario_novo);

            ctx.SaveChanges();
        }

        public List<Usuario> Listar_Todos()
        {
            return ctx.Usuarios.Include(x => x.IdTipoNavigation).Select(x => new Usuario()
            {
                IdUsuario = x.IdUsuario,
                Imagem = x.Imagem,
                Nome = x.Nome,
                Email = x.Email,
                StatusConta = x.StatusConta,
                IdTipoNavigation = new TipoUsuario()
                {
                    NomeTipo = x.IdTipoNavigation.NomeTipo
                }
            }).ToList();
        }

        public Usuario Login(string email, string senha)
        {
            var usuario = ctx.Usuarios.FirstOrDefault(u => u.Email == email);

            if (usuario != null)
            {
                if (usuario.Senha.Length < 32)
                {
                    usuario.Senha = Crypto.Gerar_Hash(usuario.Senha);
                    ctx.Usuarios.Update(usuario);
                    ctx.SaveChanges();
                }

                bool comparado = Crypto.Comparar(senha, usuario.Senha);

                if (comparado == true)
                {
                    return usuario;
                }
            }

            return null;
        }
    }
}

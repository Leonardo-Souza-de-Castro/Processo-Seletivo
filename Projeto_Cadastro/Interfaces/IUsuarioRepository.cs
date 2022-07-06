using Projeto_Cadastro.Domains;
using Projeto_Cadastro.ViewModels;
using System.Collections.Generic;

namespace Projeto_Cadastro.Interfaces
{
    public interface IUsuarioRepository
    {
        /// <summary>
        /// Metodo responsavel por listar todos os usuarios cadastrados
        /// </summary>
        /// <returns>Uma lista de usuarios</returns>
        public List<Usuario> Listar_Todos();
        
        /// <summary>
        /// Metodo responsavel por listar um usuario especifico
        /// </summary>
        /// <param name="id">Id do usuario buscado</param>
        /// <returns>Usuario encntrado</returns>
        public Usuario Buscar(int id);

        /// <summary>
        /// Metodo responsavel por cadastrar um usuario
        /// </summary>
        /// <param name="Usuario_Novo">Novo usuario a ser cadastrado</param>
        public void Cadastrar(usuarioViewModel Usuario_Novo);

        /// <summary>
        /// Metodo responsavel por Atualizar as informações de um usuario
        /// </summary>
        /// <param name="Usuario_Atualizado">Novas informações</param>
        /// <param name="id">Id do usuario a ser atualizado</param>
        /// <returns>Saber se o método deu certo ou não</returns>
        public void Editar(usuarioViewModel Usuario_Atualizado, int id);

        /// <summary>
        /// Metodo responsavel por deletar um usuario
        /// </summary>
        /// <param name="id">Id do usuario a ser excluido</param>
        public void Deletar(int id);

        /// <summary>
        /// Metodo responsavel por logar o usuario na aplicação
        /// </summary>
        /// <param name="email">Email do usuario</param>
        /// <param name="senha"></param>
        /// <returns></returns>
        public Usuario Login(string email, string senha);
    }
}

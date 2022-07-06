using Projeto_Cadastro.Domains;
using System.Collections.Generic;

namespace Projeto_Cadastro.Interfaces
{
    public interface ITipoRepository
    {
        public List<TipoUsuario> Listar();
    }
}

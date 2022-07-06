using Projeto_Cadastro.Context;
using Projeto_Cadastro.Domains;
using Projeto_Cadastro.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Projeto_Cadastro.Repositories
{
    public class TipoRepository : ITipoRepository
    {
        private readonly CadastroContext ctx;

        public TipoRepository(CadastroContext appContext)
        {
            ctx = appContext;
        }

        public List<TipoUsuario> Listar()
        {
            return ctx.TipoUsuarios.ToList();
        }
    }
}

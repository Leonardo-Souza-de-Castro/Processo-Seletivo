using System;
using System.Collections.Generic;

#nullable disable

namespace Projeto_Cadastro.Domains
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public int? IdTipo { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public bool? StatusConta { get; set; }
        public string Imagem { get; set; }

        public virtual TipoUsuario IdTipoNavigation { get; set; }
    }
}

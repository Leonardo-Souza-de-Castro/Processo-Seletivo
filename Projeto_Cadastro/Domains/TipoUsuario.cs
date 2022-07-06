using System;
using System.Collections.Generic;

#nullable disable

namespace Projeto_Cadastro.Domains
{
    public partial class TipoUsuario
    {
        public TipoUsuario()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdTipo { get; set; }
        public string NomeTipo { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}

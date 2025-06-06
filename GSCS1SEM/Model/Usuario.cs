using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCS1SEM.Model
{
    public class Usuario
    {
        public int id { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public int tipoAcesso { get; set; }


        public Usuario() { }

        public Usuario(int id, string usuario, string senha, int tipoAcesso)
        {
            this.id = id;
            this.usuario = usuario;
            this.senha = senha;
            this.tipoAcesso = tipoAcesso;
        }
    }
}

using GSCS1SEM.Model;
using GSCS1SEM.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCS1SEM.Controller
{
    public class UsuarioController
    {
        UsuarioRepository ur;

        public UsuarioController()
        {
            ur = new UsuarioRepository();
        }

        public Usuario Logar(string usuario, string senha)
        {



            var user = ur.Logar(usuario, senha);

            return user;

        }

        public void InserirUsuario(string usuario, string senha)
        {

            ur.InserirUsuario(new Usuario { usuario = usuario, senha = senha, tipoAcesso = 1 });

        }
    }
}

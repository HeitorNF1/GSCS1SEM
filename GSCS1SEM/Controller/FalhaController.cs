using GSCS1SEM.Model;
using GSCS1SEM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCS1SEM.Controller
{
    public class FalhaController
    {
        FalhaRepository repository = new FalhaRepository();
        public void InserirFalha(Falha falha)
        {
            repository.InserirFalha(falha);
        }

        public void DeletarFalha(int id)
        {
            repository.DeletarFalha(id);
        }

        public List<Falha> ListarFalhas()
        {
            return repository.GetFalhas();
        }
    }
}

using GSCS1SEM.Model;
using GSCS1SEM.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCS1SEM.Controller
{
    public class AlertaController
    {
        private readonly AlertaRepository alertaRepository;

        public AlertaController()
        {
            alertaRepository = new AlertaRepository();
        }

        public void CriarAlerta(string mensagem)
        {
            alertaRepository.InserirAlerta(mensagem);
        }

        public List<Alerta> ObterAlertas()
        {
            return alertaRepository.ListarAlertas();
        }

        public void RemoverAlerta(int id)
        {
            alertaRepository.DeletarAlerta(id);
        }
    }
}

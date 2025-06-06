using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCS1SEM.Model
{
    public class Alerta
    {
        public int Id { get; set; }
        public string Mensagem { get; set; }
        public DateTime DataCriacao { get; set; }

        public override string ToString()
        {
            return $"[{DataCriacao:dd/MM/yyyy HH:mm}] - {Mensagem}";
        }
    }
}

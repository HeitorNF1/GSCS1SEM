using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCS1SEM.Model
{
    public enum TipoFalha
    {
        Climatica,
        Cibernetica,
        Mista,
        Desconhecida
    }

    public enum SeveridadeFalha
    {
        Baixa,
        Media,
        Alta
    }

    //Fiz como eu faria em java professor. Espero que goste da organização :)
    public class Falha
    {
        public int Id { get; set; } // Gerenciado pelo banco
        public string Local { get; set; }
        public DateTime DataHora { get; set; }
        public TipoFalha Tipo { get; set; }
        public SeveridadeFalha Severidade { get; set; }
        public string Observacao { get; set; }

        public Falha()
        {
            // Construtor vazio para leitura do banco ou JSON
        }

        public Falha(string local, DateTime dataHora, TipoFalha tipo, SeveridadeFalha severidade, string observacao)
        {
            Local = local;
            DataHora = dataHora;
            Tipo = tipo;
            Severidade = severidade;
            Observacao = observacao;
        }

        public override string ToString()
        {
            return $"ID: {Id} | Local: {Local} | Data: {DataHora:G} | Tipo: {Tipo} | Severidade: {Severidade} | \nObservação: {Observacao}";
        }
    }
}

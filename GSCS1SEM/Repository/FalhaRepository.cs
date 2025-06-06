using GSCS1SEM.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCS1SEM.Repository
{
    public class FalhaRepository
    {

        public List<Falha> GetFalhas()
        {
            var falha = new List<Falha>();
            string connectionString = "User Id=rm551539;Password=100605;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    string query = "SELECT * FROM falha";
                    connection.Open();

                    using (var cmd = new OracleCommand(query, connection))
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("Nenhum dado encontrado na consulta.");
                        }

                        while (reader.Read())
                        {
                            falha.Add(new Falha
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                Local = reader["LOCAL"].ToString(),
                                DataHora = Convert.ToDateTime(reader["DATAHORA"]),
                                Tipo = Enum.TryParse(reader["TIPO"].ToString(), out TipoFalha tipo)
                                    ? tipo
                                    : TipoFalha.Desconhecida,
                                Severidade = Enum.TryParse(reader["SEVERIDADE"].ToString(), out SeveridadeFalha severidade)
                                    ? severidade
                                    : SeveridadeFalha.Baixa,
                                Observacao = reader["OBSERVACAO"]?.ToString() ?? ""
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao buscar funcionários: {ex.Message}");
                throw;
            }

            return falha;
        }

        public void InserirFalha(Falha falha)
        {
            string connectionString = "User Id=rm551539;Password=100605;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    string query = @"INSERT INTO falha (LOCAL, DATAHORA, TIPO, SEVERIDADE, OBSERVACAO)
                             VALUES (:local, :dataHora, :tipo, :severidade, :observacao)";

                    using (var cmd = new OracleCommand(query, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("local", falha.Local));
                        cmd.Parameters.Add(new OracleParameter("dataHora", falha.DataHora));
                        cmd.Parameters.Add(new OracleParameter("tipo", falha.Tipo.ToString()));
                        cmd.Parameters.Add(new OracleParameter("severidade", falha.Severidade.ToString()));
                        cmd.Parameters.Add(new OracleParameter("observacao", falha.Observacao ?? ""));

                        connection.Open();
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                            Console.WriteLine("Falha inserida com sucesso!");
                        else
                            Console.WriteLine("Nenhuma falha foi inserida.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir falha: {ex.Message}");
                throw;
            }
        }

        public void DeletarFalha(int id)
        {
            string connectionString = "User Id=rm551539;Password=100605;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    string query = "DELETE FROM falha WHERE id = :id";

                    using (var cmd = new OracleCommand(query, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", id));

                        connection.Open();
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                            Console.WriteLine("Falha deletada com sucesso!");
                        else
                            Console.WriteLine("Nenhuma falha encontrada com o ID informado.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar falha: {ex.Message}");
                throw;
            }
        }
    }
}
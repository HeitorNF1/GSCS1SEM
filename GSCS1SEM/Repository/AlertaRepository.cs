using GSCS1SEM.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCS1SEM.Repository
{
    public class AlertaRepository
    {
        private readonly string connectionString = "User Id=rm551539;Password=100605;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

        public void InserirAlerta(string mensagem)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    string query = "INSERT INTO alerta (mensagem, data_criacao) VALUES (:mensagem, :dataCriacao)";
                    using (var cmd = new OracleCommand(query, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("mensagem", mensagem));
                        cmd.Parameters.Add(new OracleParameter("dataCriacao", DateTime.Now));

                        connection.Open();
                        cmd.ExecuteNonQuery();
                        Console.WriteLine("Alerta inserido com sucesso!");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir alerta: {ex.Message}");
                throw;
            }
        }

        public List<Alerta> ListarAlertas()
        {
            List<Alerta> alertas = new List<Alerta>();

            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    string query = "SELECT * FROM alerta ORDER BY data_criacao DESC";
                    using (var cmd = new OracleCommand(query, connection))
                    {
                        connection.Open();
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var alerta = new Alerta
                                {
                                    Id = Convert.ToInt32(reader["id"]),
                                    Mensagem = reader["mensagem"].ToString(),
                                    DataCriacao = Convert.ToDateTime(reader["data_criacao"])
                                };
                                alertas.Add(alerta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao listar alertas: {ex.Message}");
                throw;
            }

            return alertas;
        }

        public void DeletarAlerta(int id)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    string query = "DELETE FROM alerta WHERE id = :id";
                    using (var cmd = new OracleCommand(query, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("id", id));

                        connection.Open();
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                            Console.WriteLine("Alerta deletado com sucesso!");
                        else
                            Console.WriteLine("Nenhum alerta encontrado com esse ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao deletar alerta: {ex.Message}");
                throw;
            }
        }
    }
}
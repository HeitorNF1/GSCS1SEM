using GSCS1SEM.Model;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GSCS1SEM.Repository
{
    public class UsuarioRepository
    {
        string connectionString = "User Id=rm551539;Password=100605;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=oracle.fiap.com.br)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=ORCL)));";

        public Usuario Logar(string usuario, string senha)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    string query = "SELECT * FROM login_gs WHERE usuario = :usuario AND senha = :senha";
                    connection.Open();

                    using (var cmd = new OracleCommand(query, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("usuario", usuario));
                        cmd.Parameters.Add(new OracleParameter("senha", senha));

                        using (var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Usuario
                                {
                                    id = Convert.ToInt32(reader["Id"]),
                                    usuario = reader["usuario"].ToString(),
                                    senha = reader["senha"].ToString(),
                                    tipoAcesso = Convert.ToInt32(reader["tipoAcesso"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao realizar login: {ex.Message}");
            }

            return null; // login falhou
        }

        public void InserirUsuario(Usuario usuario)
        {
            try
            {
                using (var connection = new OracleConnection(connectionString))
                {
                    string query = @"INSERT INTO login_gs (usuario, senha, tipoAcesso) 
                                     VALUES (:usuario, :senha, :tipoAcesso)";

                    connection.Open();

                    using (var cmd = new OracleCommand(query, connection))
                    {
                        cmd.Parameters.Add(new OracleParameter("usuario", usuario.usuario));
                        cmd.Parameters.Add(new OracleParameter("senha", usuario.senha));
                        cmd.Parameters.Add(new OracleParameter("tipoAcesso", usuario.tipoAcesso));

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        Console.WriteLine(linhasAfetadas > 0
                            ? "Usuário inserido com sucesso!"
                            : "Nenhum usuário foi inserido.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao inserir usuário: {ex.Message}");
                throw;
            }
        }

    }
}

using GSCS1SEM.Controller;
using GSCS1SEM.Model;
using GSCS1SEM.Repository;
using System;

namespace GSCS1SEM.Service
{
    public class UsuarioService
    {
        private readonly UsuarioController usuarioController;
        private readonly MenuService falhaService;

        public UsuarioService()
        {
            usuarioController = new UsuarioController();
            falhaService = new MenuService();
        }

        // Realiza o processo de login e direciona o usuário ao menu apropriado
        public Usuario Login()
        {
            Usuario usuarioLogado = null;

            while (usuarioLogado == null)
            {
                try
                {
                    Console.Write("Digite o usuário: ");
                    string usuario = Console.ReadLine();

                    Console.Write("Digite a senha: ");
                    string senha = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(senha))
                    {
                        Console.WriteLine("Usuário e senha não podem estar vazios. Tente novamente.\n");
                        continue;
                    }

                    usuarioLogado = usuarioController.Logar(usuario, senha);

                    if (usuarioLogado == null)
                    {
                        Console.WriteLine("Usuário ou senha inválidos. Tente novamente.\n");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro ao tentar realizar login: {ex.Message}\n");
                }
            }

            Console.WriteLine($"\nBem-vindo, {usuarioLogado.usuario}!");

            try
            {
                // Exibe o menu baseado no tipo de acesso (0 = admin)
                falhaService.ExibirMenu(usuarioLogado.tipoAcesso);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao carregar menu: {ex.Message}");
            }

            return usuarioLogado;
        }
    }
}

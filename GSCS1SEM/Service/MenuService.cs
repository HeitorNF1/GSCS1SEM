using GSCS1SEM.Controller;
using GSCS1SEM.Model;
using System;
using System.Collections.Generic;

namespace GSCS1SEM.Service
{
    public class MenuService
    {
        private readonly FalhaController falhaController = new FalhaController();
        private readonly AlertaController alertaController = new AlertaController();

        public void ExibirMenu(int tipoAcesso)
        {
            if (tipoAcesso == 0)
                MenuAdministrador();
            else
                MenuUsuarioComum();
        }

        private void MenuAdministrador()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n=== MENU ADMINISTRADOR ===");
                Console.WriteLine("1 - Cadastrar nova falha");
                Console.WriteLine("2 - Listar falhas");
                Console.WriteLine("3 - Deletar falha");
                Console.WriteLine("4 - Gerenciar alertas");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        CadastrarFalha();
                        break;
                    case 2:
                        ListarFalhas();
                        break;
                    case 3:
                        DeletarFalha();
                        break;
                    case 4:
                        MenuAlertas(isAdmin: true);
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcao != 0);
        }

        private void MenuUsuarioComum()
        {
            int opcao;
            do
            {
                Console.WriteLine("\n=== MENU USUÁRIO COMUM ===");
                Console.WriteLine("1 - Cadastrar nova falha");
                Console.WriteLine("2 - Ver alertas");
                Console.WriteLine("0 - Sair");
                Console.Write("Opção: ");
                int.TryParse(Console.ReadLine(), out opcao);

                switch (opcao)
                {
                    case 1:
                        CadastrarFalha();
                        break;
                    case 2:
                        MenuAlertas(isAdmin: false);
                        break;
                    case 0:
                        Console.WriteLine("Saindo...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida.");
                        break;
                }

            } while (opcao != 0);
        }

        private void MenuAlertas(bool isAdmin)
        {
            bool continuar = true;

            while (continuar)
            {
                Console.WriteLine("\n=== ALERTAS ===");
                var alertas = alertaController.ObterAlertas();

                if (alertas.Count == 0)
                    Console.WriteLine("Nenhum alerta disponível.");
                else
                {
                    foreach (var alerta in alertas)
                        Console.WriteLine($"ID: {alerta.Id} - {alerta}");
                }

                if (isAdmin)
                {
                    Console.WriteLine("\n1 - Criar novo alerta");
                    Console.WriteLine("2 - Deletar alerta");
                    Console.WriteLine("0 - Voltar");
                    Console.Write("Opção: ");
                    string opcao = Console.ReadLine();

                    switch (opcao)
                    {
                        case "1":
                            Console.Write("Digite a mensagem do alerta: ");
                            string msg = Console.ReadLine();
                            alertaController.CriarAlerta(msg);
                            break;
                        case "2":
                            Console.Write("Digite o ID do alerta a ser deletado: ");
                            if (int.TryParse(Console.ReadLine(), out int id))
                                alertaController.RemoverAlerta(id);
                            else
                                Console.WriteLine("ID inválido.");
                            break;
                        case "0":
                            continuar = false;
                            break;
                        default:
                            Console.WriteLine("Opção inválida.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("\nPressione qualquer tecla para voltar...");
                    Console.ReadKey();
                    continuar = false;
                }
            }
        }

        public Falha CadastrarFalha()
        {
            Console.WriteLine("\n=== Cadastro de Falha ===");

            string local;
            do
            {
                Console.Write("Informe o local da falha: ");
                local = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(local));

            DateTime dataHora;
            while (true)
            {
                Console.Write("Informe a data e hora da falha (dd/MM/yyyy HH:mm): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy HH:mm", null,
                    System.Globalization.DateTimeStyles.None, out dataHora))
                    break;
                Console.WriteLine("Data/hora inválida. Use o formato dd/MM/yyyy HH:mm.");
            }

            TipoFalha tipo = LerTipoFalha();
            SeveridadeFalha severidade = LerSeveridadeFalha();

            Console.Write("Informe observação (opcional): ");
            string observacao = Console.ReadLine();

            var falha = new Falha(local, dataHora, tipo, severidade, observacao);
            falhaController.InserirFalha(falha);

            Console.WriteLine("\nFalha cadastrada com sucesso!");
            Console.WriteLine(falha);
            return falha;
        }

        private void ListarFalhas()
        {
            var falhas = falhaController.ListarFalhas();
            Console.WriteLine("\n=== Lista de Falhas ===");
            foreach (var f in falhas)
            {
                Console.WriteLine(f);
            }
        }

        private void DeletarFalha()
        {
            Console.Write("\nInforme o ID da falha que deseja deletar: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                falhaController.DeletarFalha(id);
                Console.WriteLine("Falha deletada com sucesso!");
            }
            else
            {
                Console.WriteLine("ID inválido.");
            }
        }

        private TipoFalha LerTipoFalha()
        {
            while (true)
            {
                Console.WriteLine("Escolha o Tipo da Falha:");
                foreach (var tipo in Enum.GetValues(typeof(TipoFalha)))
                {
                    Console.WriteLine($"{(int)tipo} - {tipo}");
                }

                if (int.TryParse(Console.ReadLine(), out int tipoInt) &&
                    Enum.IsDefined(typeof(TipoFalha), tipoInt))
                    return (TipoFalha)tipoInt;

                Console.WriteLine("Entrada inválida, por favor escolha um número válido.");
            }
        }

        private SeveridadeFalha LerSeveridadeFalha()
        {
            while (true)
            {
                Console.WriteLine("Escolha a Severidade da Falha:");
                foreach (var severidade in Enum.GetValues(typeof(SeveridadeFalha)))
                {
                    Console.WriteLine($"{(int)severidade} - {severidade}");
                }

                if (int.TryParse(Console.ReadLine(), out int sevInt) &&
                    Enum.IsDefined(typeof(SeveridadeFalha), sevInt))
                    return (SeveridadeFalha)sevInt;

                Console.WriteLine("Entrada inválida, por favor escolha um número válido.");
            }
        }
    }
}

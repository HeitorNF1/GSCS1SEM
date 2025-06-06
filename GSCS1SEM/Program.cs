using GSCS1SEM.Controller;
using GSCS1SEM.Model;
using GSCS1SEM.Repository;
using GSCS1SEM.Service;

MenuService menu_inicial = new MenuService();
FalhaRepository usuarioRepository = new FalhaRepository();
UsuarioRepository ur = new UsuarioRepository();
UsuarioController uc = new UsuarioController();

UsuarioService us = new UsuarioService();

while (true)
{
    Console.WriteLine("Digite 1 para se cadastrar ou 2 para logar ou 0 para sair");
    string opcao = Console.ReadLine();
    if (opcao == "1")
    {
        Console.WriteLine("Digite seu nome");
        string nome = Console.ReadLine();

        Console.WriteLine("Digite sua senha");
        string senha = Console.ReadLine();

        uc.InserirUsuario(nome, senha);
    }

    else if (opcao == "2")
    {

        us.Login();

    }

    else if (opcao == "0") {
        break;
    }

    else
    {
        Console.WriteLine("Opção inválida");
    }
    
}








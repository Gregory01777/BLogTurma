using System;
using BlogDaTurma.Views;
using BlogDaTurma.Models;
using BlogDaTurma.Services;
using BlogDaTurma.Controllers;
using BlogDaTurma.Data;

class Program
{
    static void Main()
    {
        var conexao = new Conexao();
        var sessao = new SessaoUsuario(); 

        var publicacaoView = new PublicacaoView();
        var publicacaoService = new PublicacaoService(conexao);
        var publicacaoController = new PublicacaoController(publicacaoView, publicacaoService, sessao);

        var usuarioService = new UsuarioService(conexao);
        var autenticacaoView = new AutenticacaoView();
        var autenticacaoController = new AuthenticacaoController(autenticacaoView, usuarioService, sessao);

        var comentarioView = new ComentarioView();
        var comentarioService = new ComentarioService(conexao);
        var comentarioController = new ComentarioController(comentarioView, comentarioService, sessao);

        var menu = new MenuView();

        while (true)
        {
            int opcao = menu.ExibirMenu(sessao.EstaAutenticado, sessao.NomeUsuario);

            switch (opcao)
            {
                case 1:
                    Console.WriteLine("Funcionalidade: Acessar Conta (Login)");
                    autenticacaoController.Login();
                    break;

                case 2:
                    Console.WriteLine("Funcionalidade: Criar Conta (Cadastro)");
                    autenticacaoController.Cadastrar();
                    break;

                case 3:
                    publicacaoController.Listar();
                    break;

                case 4:
                    Console.WriteLine("Funcionalidade: Criar Post");
                    publicacaoController.Criar();
                    break;

                case 5:
                    Console.WriteLine("Funcionalidade: Editar Post");
                    publicacaoController.Editar();
                    break;

                case 6:
                    Console.WriteLine("Funcionalidade: Deletar Post");
                    publicacaoController.Deletar();
                    break;

                case 7:
                    Console.WriteLine("Funcionalidade: Criar Comentário");
                    comentarioController.Criar();
                    break;

                case 8:
                    Console.WriteLine("Funcionalidade: Listar Comentários");
                    comentarioController.Listar();
                    break;

                case 9:
                    Console.WriteLine("Funcionalidade: Deletar Comentário");
                    comentarioController.Deletar();
                    break;

                case 0:
                    Console.WriteLine("Encerrando a aplicação... Até mais!");
                    return;

                default:
                    Console.WriteLine("Opção inválida! Pressione qualquer tecla para tentar novamente.");
                    Console.ReadKey();
                    break;
            }
        }
    }
}
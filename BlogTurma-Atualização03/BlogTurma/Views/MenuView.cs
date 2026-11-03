using System;

namespace BlogDaTurma.Views;

public class MenuView
{
    public int ExibirMenu(bool estaAutenticado, string? nomeUsuario)
    {
        Console.WriteLine();
        Console.WriteLine("========================================");
        Console.WriteLine("             BLOG DA TURMA              ");
        Console.WriteLine("========================================");
        
        if (estaAutenticado)
        {
            Console.WriteLine($" Status: Logado como [{nomeUsuario}]");
        }
        else
        {
            Console.WriteLine(" Status: Visitante (Não autenticado)");
        }

        Console.WriteLine("----------------------------------------");
        Console.WriteLine(" [1] Acessar Conta (Login)");
        Console.WriteLine(" [2] Criar Conta (Cadastro)");
        Console.WriteLine(" [3] Listar Posts");
        Console.WriteLine(" [4] Criar Post");
        Console.WriteLine(" [5] Editar Post");
        Console.WriteLine(" [6] Deletar Post");
        Console.WriteLine(" [7] Criar Comentário");
        Console.WriteLine(" [8] Listar Comentários");
        Console.WriteLine(" [9] Deletar Comentário");
        Console.WriteLine(" [0] Sair");
        Console.WriteLine("========================================");
        Console.Write("Digite o número da opção desejada: ");

        string? entrada = Console.ReadLine();

        if (int.TryParse(entrada, out int opcao))
        {
            return opcao;
        }

        return -1;
    }
}
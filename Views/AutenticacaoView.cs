using System;

namespace BlogTurma.Views
{
    public class AutenticacaoView
    {
        public (string Nome, string Email, string Senha) SolicitarDadosCadastro()
        {
            Console.WriteLine("=== CADASTRO DE USUÁRIO ===");
            Console.Write("Nome: ");
            string nome = Console.ReadLine();

            Console.Write("E-mail: ");
            string email = Console.ReadLine();

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            return (nome, email, senha);
        }

        public (string Email, string Senha) SolicitarCredenciais()
        {
            Console.WriteLine("=== LOGIN ===");
            Console.Write("E-mail: ");
            string email = Console.ReadLine();

            Console.Write("Senha: ");
            string senha = Console.ReadLine();

            return (email, senha);
        }

        public void ExibirMensagem(string mensagem, bool sucesso = true)
        {
            Console.ForegroundColor = sucesso ? ConsoleColor.Green : ConsoleColor.Red;
            Console.WriteLine($"\n> {mensagem}\n");
            Console.ResetColor();
        }
    }
}
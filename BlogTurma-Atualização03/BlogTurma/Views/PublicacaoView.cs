using BlogDaTurma.Models;

namespace BlogDaTurma.Views;

public class PublicacaoView
{
    public void ExibirPublicacoes(List<Publicacao> publicacoes)
    {
        Console.WriteLine("\n--- LISTA DE POSTS ---");
        if (publicacoes == null || publicacoes.Count == 0)
        {
            Console.WriteLine("Nenhum post encontrado.");
            return;
        }

        foreach (var pub in publicacoes)
        {
            Console.WriteLine($"\n[ID #{pub.Id}] {pub.Titulo}");
            Console.WriteLine($"Data: {pub.DataCriacao:dd/MM/yyyy HH:mm} | Autor ID: {pub.UsuarioId}");
            Console.WriteLine($"Conteúdo: {pub.Texto}");
            Console.WriteLine(new string('-', 40));
        }
    }

    public (string Titulo, string Texto) SolicitarDadosCriacao()
    {
        Console.WriteLine("\n--- CRIAR NOVO POST ---");
        Console.Write("Título: ");
        string titulo = Console.ReadLine() ?? string.Empty;

        Console.Write("Texto: ");
        string texto = Console.ReadLine() ?? string.Empty;

        return (titulo, texto);
    }

    public (int Id, string Titulo, string Texto) SolicitarDadosEdicao()
    {
        Console.WriteLine("\n--- EDITAR POST ---");
        Console.Write("Informe o ID do post que deseja editar: ");
        int.TryParse(Console.ReadLine(), out int id);

        Console.Write("Novo Título: ");
        string titulo = Console.ReadLine() ?? string.Empty;

        Console.Write("Novo Texto: ");
        string texto = Console.ReadLine() ?? string.Empty;

        return (id, titulo, texto);
    }

    public int SolicitarIdExclusao()
    {
        Console.WriteLine("\n--- DELETAR POST ---");
        Console.Write("Informe o ID do post que deseja excluir: ");
        int.TryParse(Console.ReadLine(), out int id);
        return id;
    }

    public void ExibirMensagem(string mensagem)
    {
        Console.WriteLine($"[Aviso]: {mensagem}");
    }
}
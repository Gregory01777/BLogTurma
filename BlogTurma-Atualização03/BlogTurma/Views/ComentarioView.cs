using BlogDaTurma.Models;

namespace BlogDaTurma.Views;

public class ComentarioView
{
    public (int PublicacaoId, string Texto) SolicitarDadosCriacao()
    {
        Console.WriteLine("\n--- CRIAR COMENTÁRIO ---");
        Console.Write("ID do Post: ");
        int.TryParse(Console.ReadLine(), out int pubId);

        Console.Write("Comentário: ");
        string texto = Console.ReadLine() ?? string.Empty;

        return (pubId, texto);
    }

    public int SolicitarPublicacaoId()
    {
        Console.WriteLine("\n--- LISTAR COMENTÁRIOS DE UM POST ---");
        Console.Write("Informe o ID do Post: ");
        int.TryParse(Console.ReadLine(), out int pubId);
        return pubId;
    }

    public void ExibirComentarios(List<Comentario> comentarios)
    {
        Console.WriteLine("\n--- COMENTÁRIOS ---");
        if (comentarios == null || comentarios.Count == 0)
        {
            Console.WriteLine("Nenhum comentário encontrado para este post.");
            return;
        }

        foreach (var c in comentarios)
        {
            Console.WriteLine($"\n[Comentário ID #{c.Id}] Autor ID: {c.UsuarioId} | Data: {c.DataCriacao:dd/MM/yyyy HH:mm}");
            Console.WriteLine($"Texto: {c.Texto}");
            Console.WriteLine(new string('-', 30));
        }
    }

    public int SolicitarIdExclusao()
    {
        Console.WriteLine("\n--- DELETAR COMENTÁRIO ---");
        Console.Write("Informe o ID do comentário que deseja excluir: ");
        int.TryParse(Console.ReadLine(), out int id);
        return id;
    }

    public void ExibirMensagem(string mensagem)
    {
        Console.WriteLine($"[Aviso]: {mensagem}");
    }
}
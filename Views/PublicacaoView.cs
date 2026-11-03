using BlogDaTurma.Models;

public class PublicacaoView
{
    public void ExibirPublicacoes(List<Publicacao> publicacoes)
    {
        if (publicacoes == null || publicacoes.Count == 0)
        {
            Console.WriteLine("Nenhuma publicação encontrada. ");
            return;
        }

        Console.WriteLine("---PUBLICAÇÕES RECENTES---");
        foreach (var pub in publicacoes)
        {
            Console.WriteLine($"[{pub.Id}] {pub.Titulo}");
            Console.WriteLine($"Data : {pub.DataCriacao:dd/mm/yyyy HH:mm} | Autor : {pub.UsuarioId}");
            Console.WriteLine($"Texto : {pub.Texto}");
            Console.WriteLine(new string('-', 30));
        }
    }
}
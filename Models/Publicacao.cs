namespace BlogDaTurma.Models;

public class Publicacao
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    public string Conteudo { get; set;} = string.Empty;
    public DateTime DataCriacao { get; set; }
    public int UsuarioId { get; set;}
    
}
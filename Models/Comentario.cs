namespace BlogDaTurma.Models;

public class Comentario
{
    public int Id { get;  set; }
    public string Texto { get; set; } = string.Empty;
    public DateTime DataCriação { get; set;}
    public int UsuarioId { get; set; }
    public int PublicacaoId { get; set; }
}
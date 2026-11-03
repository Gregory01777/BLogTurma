using BlogDaTurma.Data;
using BlogDaTurma.Models;
using MySqlConnector;
namespace BlogDaTurma.Services;

public class ComentarioService
{
    private readonly Conexao _conexao;

    public ComentarioService(Conexao conexao)
    {
        _conexao = conexao;
    }

    public void Criar(string texto, int publicacaoId, int usuarioId)
    {
        if (string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException("O texto do comentário é obrigatório.");
        }

        using var conn = _conexao.CriarConexao();
        conn.Open();

        string sql = "INSERT INTO comentario (texto, data_criacao, usuario_id, publicacao_id) VALUES (@texto, @data, @usuarioId, @publicacaoId)";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@texto", texto.Trim());
        cmd.Parameters.AddWithValue("@data", DateTime.Now);
        cmd.Parameters.AddWithValue("@usuarioId", usuarioId);
        cmd.Parameters.AddWithValue("@publicacaoId", publicacaoId);

        cmd.ExecuteNonQuery();
    }

    public List<Comentario> ListarPorPublicacao(int publicacaoId)
    {
        var comentarios = new List<Comentario>();

        using var conn = _conexao.CriarConexao();
        conn.Open();

        string sql = "SELECT id, texto, data_criacao, usuario_id, publicacao_id FROM comentario WHERE publicacao_id = @publicacaoId ORDER BY data_criacao ASC";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@publicacaoId", publicacaoId);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            comentarios.Add(new Comentario
            {
                Id = reader.GetInt32("id"),
                Texto = reader.GetString("texto"),
                DataCriacao = reader.GetDateTime("data_criacao"),
                UsuarioId = reader.GetInt32("usuario_id"),
                PublicacaoId = reader.GetInt32("publicacao_id")
            });
        }

        return comentarios;
    }

    public void Deletar(int id, int usuarioId)
    {
        using var conn = _conexao.CriarConexao();
        conn.Open();

        string sql = "DELETE FROM comentario WHERE id = @id AND usuario_id = @usuarioId";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@usuarioId", usuarioId);

        int linhasAfetadas = cmd.ExecuteNonQuery();
        if (linhasAfetadas == 0)
        {
            throw new InvalidOperationException("Comentário não encontrado ou sem permissão para eliminar.");
        }
    }
}
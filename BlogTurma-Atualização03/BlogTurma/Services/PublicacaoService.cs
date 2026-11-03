using BlogDaTurma.Data;
using BlogDaTurma.Models;
using MySqlConnector;

namespace BlogDaTurma.Services;

public class PublicacaoService
{
    private readonly Conexao _conexao;

    public PublicacaoService(Conexao conexao)
    {
        _conexao = conexao;
    }

    public List<Publicacao> ListarRecentes()
    {
        var publicacoes = new List<Publicacao>();

        using var conn = _conexao.CriarConexao();
        conn.Open();

        string sql = "SELECT id, titulo, texto, data_criacao, usuario_id FROM publicacao ORDER BY data_criacao DESC";
        using var cmd = new MySqlCommand(sql, conn);
        using var reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            publicacoes.Add(new Publicacao
            {
                Id = reader.GetInt32("id"),
                Titulo = reader.GetString("titulo"),
                Texto = reader.GetString("texto"),
                DataCriacao = reader.GetDateTime("data_criacao"),
                UsuarioId = reader.GetInt32("usuario_id")
            });
        }

        return publicacoes;
    }

    public void Criar(string titulo, string texto, int usuarioId)
    {
        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException("Título e texto são obrigatórios.");
        }

        using var conn = _conexao.CriarConexao();
        conn.Open();

        string sql = "INSERT INTO publicacao (titulo, texto, data_criacao, usuario_id) VALUES (@titulo, @texto, @data, @usuarioId)";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@titulo", titulo.Trim());
        cmd.Parameters.AddWithValue("@texto", texto.Trim());
        cmd.Parameters.AddWithValue("@data", DateTime.Now);
        cmd.Parameters.AddWithValue("@usuarioId", usuarioId);

        cmd.ExecuteNonQuery();
    }

    public void Editar(int id, string titulo, string texto, int usuarioId)
    {
        if (string.IsNullOrWhiteSpace(titulo) || string.IsNullOrWhiteSpace(texto))
        {
            throw new ArgumentException("Título e texto são obrigatórios.");
        }

        using var conn = _conexao.CriarConexao();
        conn.Open();

        string sql = "UPDATE publicacao SET titulo = @titulo, texto = @texto WHERE id = @id AND usuario_id = @usuarioId";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@titulo", titulo.Trim());
        cmd.Parameters.AddWithValue("@texto", texto.Trim());
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@usuarioId", usuarioId);

        int linhasAfetadas = cmd.ExecuteNonQuery();
        if (linhasAfetadas == 0)
        {
            throw new InvalidOperationException("Publicação não encontrada ou sem permissão para editar.");
        }
    }

    public void Deletar(int id, int usuarioId)
    {
        using var conn = _conexao.CriarConexao();
        conn.Open();

        string sql = "DELETE FROM publicacao WHERE id = @id AND usuario_id = @usuarioId";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@usuarioId", usuarioId);

        int linhasAfetadas = cmd.ExecuteNonQuery();
        if (linhasAfetadas == 0)
        {
            throw new InvalidOperationException("Publicação não encontrada ou sem permissão para eliminar.");
        }
    }
}
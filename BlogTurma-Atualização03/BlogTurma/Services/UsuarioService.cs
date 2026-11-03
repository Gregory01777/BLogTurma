using BlogDaTurma.Data;
using BlogDaTurma.Models;
using MySqlConnector;

namespace BlogDaTurma.Services;

public class UsuarioService
{
    private readonly Conexao _conexao;
    private readonly SenhaService _senhaService;

    public UsuarioService(Conexao conexao)
    {
        _conexao = conexao;
        _senhaService = new SenhaService();
    }

    public void Cadastrar(string nome, string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
            throw new ArgumentException("Todos os campos são obrigatórios.");
        }

        string emailNormalizado = email.Trim().ToLowerInvariant();

        using var conn = _conexao.CriarConexao();
        conn.Open();

        string checkSql = "SELECT COUNT(*) FROM usuario WHERE LOWER(email) = @email";
        using (var checkCmd = new MySqlCommand(checkSql, conn))
        {
            checkCmd.Parameters.AddWithValue("@email", emailNormalizado);
            var result = checkCmd.ExecuteScalar();
            long count = result != null ? Convert.ToInt64(result) : 0;
            if (count > 0)
            {
                throw new InvalidOperationException("O e-mail informado já se encontra registado.");
            }
        }

        string senhaHash = _senhaService.GerarHash(senha);

        string insertSql = "INSERT INTO usuario (nome, email, senha_hash) VALUES (@nome, @email, @senhaHash)";
        using var insertCmd = new MySqlCommand(insertSql, conn);
        insertCmd.Parameters.AddWithValue("@nome", nome.Trim());
        insertCmd.Parameters.AddWithValue("@email", emailNormalizado);
        insertCmd.Parameters.AddWithValue("@senhaHash", senhaHash);

        insertCmd.ExecuteNonQuery();
    }

    public Usuario? Autenticar(string email, string senha)
    {
        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
        {
            return null;
        }

        string emailNormalizado = email.Trim().ToLowerInvariant();

        using var conn = _conexao.CriarConexao();
        conn.Open();

        string sql = "SELECT id, nome, email, senha_hash FROM usuario WHERE LOWER(email) = @email";
        using var cmd = new MySqlCommand(sql, conn);
        cmd.Parameters.AddWithValue("@email", emailNormalizado);

        using var reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            string hashArmazenado = reader.GetString("senha_hash");

            if (_senhaService.Verificar(senha, hashArmazenado))
            {
                return new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Email = reader.GetString("email"),
                    SenhaHash = hashArmazenado
                };
            }
        }

        return null;
    }
}
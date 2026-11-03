using MySqlConnector;

namespace BlogDaTurma.Data;

public class Conexao
{
    private readonly string _connectionString;

    public Conexao()
    {
        var host = LerVariavel("DB_HOST", "localhost");
        var port = LerVariavel("DB_PORT", "3306");
        var database = LerVariavel("DB_NAME", "blog_turma");
        var user = LerVariavel("DB_USER", "root");
        var password = LerVariavel("DB_PASSWORD", "root");

        _connectionString = $"Server={host};Port={port};Database={database};Uid={user};Pwd={password};";
    }

    public MySqlConnection CriarConexao()
    {
        return new MySqlConnection(_connectionString);
    }

    private string LerVariavel(string chave, string valorPadrao)
    {
        var valor = Environment.GetEnvironmentVariable(chave);
        return string.IsNullOrWhiteSpace(valor) ? valorPadrao : valor;
    }
}
using MySqlConnector;
using BlogTurma.Data;
using BlogTurma.Models;

namespace BlogTurma.Services
{
    public class UsuarioService
    {
        private readonly Conexao _conexao;

        public UsuarioService(Conexao conexao)
        {
            _conexao = conexao;
        }

        public bool Cadastrar(string nome, string email, string senha)
        {
            using var conn = _conexao.CriarConexao();
            conn.Open();

            // Evitar e-mail duplicado
            string sqlVerifica = "SELECT COUNT(*) FROM usuarios WHERE email = @email;";
            using var cmdVerifica = new MySqlCommand(sqlVerifica, conn);
            cmdVerifica.Parameters.AddWithValue("@email", email);

            if ((long)cmdVerifica.ExecuteScalar() > 0)
                return false; // E-mail já existe

            // Gravar usuário no MySQL
            string sql = "INSERT INTO usuarios (nome, email, senhahash) VALUES (@nome, @email, @senha);";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@nome", nome);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            return cmd.ExecuteNonQuery() > 0;
        }

        public Usuario Autenticar(string email, string senha)
        {
            using var conn = _conexao.CriarConexao();
            conn.Open();

            string sql = "SELECT id, nome, email, senhahash FROM usuarios WHERE email = @email AND senhahash = @senha;";
            using var cmd = new MySqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@email", email);
            cmd.Parameters.AddWithValue("@senha", senha);

            using var reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                return new Usuario
                {
                    Id = reader.GetInt32("id"),
                    Nome = reader.GetString("nome"),
                    Email = reader.GetString("email"),
                    SenhaHash = reader.GetString("senhahash")
                };
            }

            return null; // Credenciais inválidas
        }
    }
}
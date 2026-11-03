using System;
using MySqlConnector; 

namespace BlogTurma.Data
{
    public class Conexao
    {
        private readonly string _conexao = @"server=localhost;port=3306;database=BlogDaTurma;user=root;password=123"; 

        public MySqlConnection CriarConexao()
        {
            return new MySqlConnection(_conexao);
        }

        public void TesteConexao()
        {
            using (MySqlConnection conn = CriarConexao())
            {
                try
                {
                    conn.Open();
                    Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao conectar ao banco de dados: " + ex.Message);
                }
            }
        }
    }
}
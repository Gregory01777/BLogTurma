namespace BlogTurma.Services
{
    public class SessaoUsuario
    {
        public int UsuarioId { get; private set; }
        public string NomeUsuario { get; private set; } = string.Empty;
        public bool EstaAutenticado => UsuarioId > 0;

        public void Iniciar(int id, string nome)
        {
            UsuarioId = id;
            NomeUsuario = nome;
        }

        public void Encerrar()
        {
            UsuarioId = 0;
            NomeUsuario = string.Empty;
        }
    }
}
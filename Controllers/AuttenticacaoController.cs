using BlogTurma.Views;
using BlogTurma.Services;

namespace BlogTurma.Controllers
{
    public class AutenticacaoController
    {
        private readonly AutenticacaoView _view;
        private readonly UsuarioService _usuarioService;
        private readonly SessaoUsuario _sessao;

        public AutenticacaoController(AutenticacaoView view, UsuarioService usuarioService, SessaoUsuario sessao)
        {
            _view = view;
            _usuarioService = usuarioService;
            _sessao = sessao;
        }

        public void Cadastrar()
        {
            var (nome, email, senha) = _view.SolicitarDadosCadastro();
            bool sucesso = _usuarioService.Cadastrar(nome, email, senha);

            if (sucesso)
                _view.ExibirMensagem("Usuário cadastrado com sucesso!");
            else
                _view.ExibirMensagem("Erro ao cadastrar ou e-mail já cadastrado.", sucesso: false);
        }

        public void Entrar()
        {
            var (email, senha) = _view.SolicitarCredenciais();
            var usuario = _usuarioService.Autenticar(email, senha);

            if (usuario != null)
            {
                _sessao.Iniciar(usuario.Id, usuario.Nome);
                _view.ExibirMensagem($"Bem-vindo(a), {usuario.Nome}!");
            }
            else
            {
                _view.ExibirMensagem("E-mail ou senha inválidos.", sucesso: false);
            }
        }

        public void Sair()
        {
            _sessao.Encerrar();
            _view.ExibirMensagem("Sessão encerrada.");
        }
    }
}
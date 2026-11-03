using System;
using BlogDaTurma.Services;
using BlogDaTurma.Views;

namespace BlogDaTurma.Controllers;

public class AuthenticacaoController
{
    private readonly AutenticacaoView _view;
    private readonly UsuarioService _usuarioService;
    private readonly SessaoUsuario _sessaoUsuario;

    public AuthenticacaoController(AutenticacaoView view, UsuarioService usuarioService, SessaoUsuario sessaoUsuario)
    {
        _view = view;
        _usuarioService = usuarioService;
        _sessaoUsuario = sessaoUsuario;
    }

    public void Cadastrar()
    {
        var (nome, email, senha) = _view.SolicitarDadosCadastro();

        try
        {
            _usuarioService.Cadastrar(nome, email, senha);
            _view.ExibirMensagem("Conta criada com sucesso! Agora você pode acessar sua conta.");
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao criar conta: {ex.Message}");
        }
    }

    public void Login()
    {
        var (email, senha) = _view.SolicitarCredenciais();

        try
        {
            var usuario = _usuarioService.Autenticar(email, senha);
            if (usuario != null)
            {
                _sessaoUsuario.Iniciar(usuario);
                _view.ExibirMensagem($"Bem-vindo(a), {usuario.Nome}!");
            }
            else
            {
                _view.ExibirMensagem("Credenciais inválidas. Verifique o e-mail e a senha.");
            }
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao autenticar: {ex.Message}");
        }
    }

    public void Entrar() => Login();

    public void Sair()
    {
        if (_sessaoUsuario.EstaAutenticado)
        {
            _sessaoUsuario.Encerrar();
            _view.ExibirMensagem("Sessão encerrada com sucesso.");
        }
        else
        {
            _view.ExibirMensagem("Nenhum usuário logado no momento.");
        }
    }
}
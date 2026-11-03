using BlogDaTurma.Services;
using BlogDaTurma.Views;

namespace BlogDaTurma.Controllers;

public class PublicacaoController
{
    private readonly PublicacaoView _view;
    private readonly PublicacaoService _service;
    private readonly SessaoUsuario _sessao;

    public PublicacaoController(PublicacaoView view, PublicacaoService service, SessaoUsuario sessao)
    {
        _view = view;
        _service = service;
        _sessao = sessao;
    }

    public void Listar()
    {
        try
        {
            var publicacoes = _service.ListarRecentes();
            _view.ExibirPublicacoes(publicacoes);
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao procurar posts: {ex.Message}");
        }
    }

    public void Criar()
    {
        if (!_sessao.EstaAutenticado || !_sessao.UsuarioId.HasValue)
        {
            _view.ExibirMensagem("Precisa de estar autenticado para criar um post.");
            return;
        }

        var (titulo, texto) = _view.SolicitarDadosCriacao();

        try
        {
            _service.Criar(titulo, texto, _sessao.UsuarioId.Value);
            _view.ExibirMensagem("Post criado com sucesso!");
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao criar post: {ex.Message}");
        }
    }

    public void Editar()
    {
        if (!_sessao.EstaAutenticado || !_sessao.UsuarioId.HasValue)
        {
            _view.ExibirMensagem("Precisa de estar autenticado para editar posts.");
            return;
        }

        var (id, titulo, texto) = _view.SolicitarDadosEdicao();

        try
        {
            _service.Editar(id, titulo, texto, _sessao.UsuarioId.Value);
            _view.ExibirMensagem("Post editado com sucesso!");
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao editar post: {ex.Message}");
        }
    }

    public void Deletar()
    {
        if (!_sessao.EstaAutenticado || !_sessao.UsuarioId.HasValue)
        {
            _view.ExibirMensagem("Precisa de estar autenticado para eliminar posts.");
            return;
        }

        int id = _view.SolicitarIdExclusao();

        try
        {
            _service.Deletar(id, _sessao.UsuarioId.Value);
            _view.ExibirMensagem("Post eliminado com sucesso!");
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao eliminar post: {ex.Message}");
        }
    }
}
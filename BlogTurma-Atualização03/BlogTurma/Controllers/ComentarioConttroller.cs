using BlogDaTurma.Services;
using BlogDaTurma.Views;

namespace BlogDaTurma.Controllers;

public class ComentarioController
{
    private readonly ComentarioView _view;
    private readonly ComentarioService _service;
    private readonly SessaoUsuario _sessao;

    public ComentarioController(ComentarioView view, ComentarioService service, SessaoUsuario sessao)
    {
        _view = view;
        _service = service;
        _sessao = sessao;
    }

    public void Criar()
    {
        if (!_sessao.EstaAutenticado || !_sessao.UsuarioId.HasValue)
        {
            _view.ExibirMensagem("Precisa de estar autenticado para criar um comentário.");
            return;
        }

        var (pubId, texto) = _view.SolicitarDadosCriacao();

        try
        {
            _service.Criar(texto, pubId, _sessao.UsuarioId.Value);
            _view.ExibirMensagem("Comentário adicionado com sucesso!");
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao adicionar comentário: {ex.Message}");
        }
    }

    public void Listar()
    {
        int pubId = _view.SolicitarPublicacaoId();

        try
        {
            var comentarios = _service.ListarPorPublicacao(pubId);
            _view.ExibirComentarios(comentarios);
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao procurar comentários: {ex.Message}");
        }
    }

    public void Deletar()
    {
        if (!_sessao.EstaAutenticado || !_sessao.UsuarioId.HasValue)
        {
            _view.ExibirMensagem("Precisa de estar autenticado para eliminar comentários.");
            return;
        }

        int id = _view.SolicitarIdExclusao();

        try
        {
            _service.Deletar(id, _sessao.UsuarioId.Value);
            _view.ExibirMensagem("Comentário eliminado com sucesso!");
        }
        catch (Exception ex)
        {
            _view.ExibirMensagem($"Erro ao eliminar comentário: {ex.Message}");
        }
    }
}
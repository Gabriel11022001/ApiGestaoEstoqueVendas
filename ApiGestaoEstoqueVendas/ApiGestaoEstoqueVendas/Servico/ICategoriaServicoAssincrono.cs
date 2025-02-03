using ApiGestaoEstoqueVendas.DTO;

namespace ApiGestaoEstoqueVendas.Servico
{
    public interface ICategoriaServicoAssincrono
    {

        Task<RespostaHttp<CategoriaDTO>> CadastrarCategoriaAssincrono(CategoriaDTO categoriaDTO);

        Task<RespostaHttp<CategoriaDTO>> EditarCategoriaAssincrono(CategoriaDTO categoriaDTO);

        Task<RespostaHttp<CategoriaDTO>> BuscarCategoriaPeloIdAssincrono(int idCategoriaConsultar);

        Task<RespostaHttp<Boolean>> DeletarCategoriaAssincrono(int idCategoriaDeletar);

        Task<RespostaHttp<RetornoListagemPaginadaDTO<CategoriaDTO>>> BuscarCategoriasAssincrono(int paginaAtual, int elementosPorPagina);

        Task<RespostaHttp<RetornoListagemPaginadaDTO<CategoriaDTO>>> BuscarCategoriasFiltroAssincrono(FiltroCategoriasDTO filtroCategoriasDTO);

        Task<RespostaHttp<CategoriaDTO>> BuscarCategoriaPeloIdAssincronoTesteException(int idCategoria);

    }
}

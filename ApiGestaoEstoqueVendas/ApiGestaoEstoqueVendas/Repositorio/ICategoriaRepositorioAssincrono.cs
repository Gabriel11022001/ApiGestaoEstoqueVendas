using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;

namespace ApiGestaoEstoqueVendas.Repositorio
{
    public interface ICategoriaRepositorioAssincrono
    {

        Task CadastrarCategoriaAssincrono(Categoria categoria);

        Task EditarCategoriaAssincrono(Categoria categoria);

        Task<List<Categoria>> BuscarCategoriasAssincrono(int paginaAtual, int elementosPorPagina);

        Task DeletarCategoriaAssincrono(Categoria categoria);

        Task<Categoria> BuscarCategoriaPeloIdAssincrono(int id);

        Task<Categoria> BuscarCategoriaPeloNome(String nomeCategoriaFiltrar);

        Task<List<Categoria>> FiltrarCategoriasAssincrono(int paginaAtual, int elementosPorPagina, FiltroCategoriasDTO filtroCategoriasDTO);

    }
}

using ApiGestaoEstoqueVendas.Model;

namespace ApiGestaoEstoqueVendas.Repositorio
{
    public interface ICategoriaRepositorio<Categoria>: IRepositorioDeletar<Categoria>, IRepositorioEditar<Categoria>
    {

        List<Categoria> FiltrarCategoriasPeloNome(String nomeCategoriaFiltrar);

        List<Categoria> BuscarCategoriasAtivas();

        List<Categoria> BuscarCategoriasInativas();

        Categoria BuscarCategoriaPeloId(int idCategoria);

        void AlterarStatusCategoria(int idCategoria, Boolean novoStatus);

    }
}

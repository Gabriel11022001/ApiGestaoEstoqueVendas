using ApiGestaoEstoqueVendas.Contexto;
using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoEstoqueVendas.Repositorio
{
    public class CategoriaRepositorioAssincrono : ICategoriaRepositorioAssincrono
    {

        private ApiGestaoEstoqueVendasAppDbContexto _contexto;

        public CategoriaRepositorioAssincrono(ApiGestaoEstoqueVendasAppDbContexto contexto)
        {
            this._contexto = contexto;
        }

        // buscar categoria pelo id de forma assincrona
        public async Task<Categoria> BuscarCategoriaPeloIdAssincrono(int id)
        {

            return await this._contexto.Categorias.FindAsync(id);
        }

        // buscar categoria pelo nome assincrono
        public async Task<Categoria> BuscarCategoriaPeloNome(string nomeCategoriaFiltrar)
        {

            return await this._contexto
                .Categorias
                .FirstOrDefaultAsync(c => c.Nome.Equals(nomeCategoriaFiltrar.Trim()));
        }

        public Task<List<Categoria>> BuscarCategoriasAssincrono(int paginaAtual, int elementosPorPagina)
        {

            throw new NotImplementedException();
        }

        // cadastrar categoria de forma assincrona
        public async Task CadastrarCategoriaAssincrono(Categoria categoria)
        {
            this._contexto.Categorias.Add(categoria);
            await this._contexto.SaveChangesAsync();
        }

        // deletar a categoria de forma assincrona
        public async Task DeletarCategoriaAssincrono(Categoria categoria)
        {
            this._contexto.Categorias.Entry(categoria).State = EntityState.Deleted;
            await this._contexto.SaveChangesAsync();
        }

        // editar a categoria de forma assincrona
        public async Task EditarCategoriaAssincrono(Categoria categoria)
        {
            this._contexto.Categorias.Entry(categoria).State = EntityState.Modified;
            await this._contexto.SaveChangesAsync();
        }

        public async Task<List<Categoria>> FiltrarCategoriasAssincrono(int paginaAtual, int elementosPorPagina, FiltroCategoriasDTO filtroCategoriasDTO)
        {

            throw new NotImplementedException();
        }

    }
}

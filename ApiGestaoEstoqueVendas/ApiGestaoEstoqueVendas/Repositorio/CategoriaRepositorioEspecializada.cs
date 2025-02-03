using ApiGestaoEstoqueVendas.Contexto;
using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Utils;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoEstoqueVendas.Repositorio
{
    public class CategoriaRepositorioEspecializada : RepositorioBase<Categoria>
    {

        private ApiGestaoEstoqueVendasAppDbContexto _contexto;

        public CategoriaRepositorioEspecializada(ApiGestaoEstoqueVendasAppDbContexto contexto)
        {
            this._contexto = contexto;
        }

        public async override Task<Categoria> BuscarPeloId(int id)
        {
            Categoria categoria = await this._contexto.Categorias.FindAsync(id);

            if (categoria is null)
            {

                throw new CategoriaNaoExisteException();
            }

            return categoria;
        }

        public async override Task<List<Categoria>> BuscarTodos()
        {
            var categorias = await this._contexto.Categorias.ToListAsync();

            return categorias;
        }

        public async override Task Cadastrar(Categoria model)
        {

        }

        public async override Task Deletar(Categoria modelDeletar)
        {

        }

        public async override Task Editar(Categoria model)
        {

        }

    }
}

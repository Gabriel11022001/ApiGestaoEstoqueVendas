using ApiGestaoEstoqueVendas.Contexto;
using ApiGestaoEstoqueVendas.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoEstoqueVendas.Repositorio
{
    public class CategoriaRepositorio : ICategoriaRepositorio<Categoria>
    {

        private ApiGestaoEstoqueVendasAppDbContexto _contexto;

        public CategoriaRepositorio(ApiGestaoEstoqueVendasAppDbContexto contexto)
        {
            this._contexto = contexto;
        }

        public void AlterarStatusCategoria(int idCategoria, Boolean novoStatus)
        {
            Categoria categoria = this._contexto.Categorias.Find(idCategoria);

            if (categoria is not null)
            {
                categoria.Ativo = novoStatus;

                return;
            }

            throw new Exception("Não existe uma categoria cadastrada com esse id na base de dados!");
        }

        public Categoria BuscarCategoriaPeloId(int idCategoria)
        {
            Categoria categoria = this._contexto.Categorias.Find(idCategoria);

            return categoria;
        }

        public List<Categoria> BuscarCategoriasAtivas()
        {

            return this._contexto.Categorias
                .OrderBy(c => c.Nome)
                .Where(c => c.Ativo)
                .ToList();
        }

        public List<Categoria> BuscarCategoriasInativas()
        {

            return this._contexto.Categorias
                .OrderBy(c => c.Nome)
                .Where(c => !c.Ativo)
                .ToList();
        }

        public Categoria BuscarPeloId(int id)
        {

            return this._contexto.Categorias.Find(id);
        }

        public List<Categoria> BuscarTodos()
        {

            // depois vou implementar paginação
            return this._contexto.Categorias
                .OrderBy(c => c.Nome)
                .ToList();
        }

        public void Cadastrar(Categoria categoria)
        {
            this._contexto.Categorias.Add(categoria);
            this._contexto.SaveChanges();
        }

        public void Deletar(Categoria categoriaDeletar)
        {
            this._contexto.Categorias.Entry(categoriaDeletar).State = EntityState.Deleted;
            this._contexto.SaveChanges();
        }

        public void Editar(Categoria categoriaEditar)
        {
            this._contexto.Categorias.Entry(categoriaEditar).State = EntityState.Modified;
            this._contexto.SaveChanges();
        }

        public List<Categoria> FiltrarCategoriasPeloNome(string nomeCategoriaFiltrar)
        {
            // vou aplicar ordenação depois

            return this._contexto
                .Categorias
                .OrderBy(c => c.Nome)
                .Where(c => c.Nome.Contains(nomeCategoriaFiltrar.Trim()))
                .ToList();
        }

    }
}

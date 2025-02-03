using ApiGestaoEstoqueVendas.Contexto;
using ApiGestaoEstoqueVendas.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoEstoqueVendas.Repositorio
{
    public class ProdutoRepositorio : IRepositorioProduto<Produto>
    {

        private ApiGestaoEstoqueVendasAppDbContexto _contexto;

        public ProdutoRepositorio(ApiGestaoEstoqueVendasAppDbContexto contexto)
        {
            this._contexto = contexto;
        }

        public Produto BuscarPeloId(int id)
        {

            return this._contexto.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefault(p => p.Id == id);
        }

        public Produto BuscarProdutoPeloNome(string nomeProduto)
        {

            return this._contexto.Produtos
                .FirstOrDefault(p => p.Nome.Equals(nomeProduto));
        }

        public List<Produto> BuscarProdutosEntrePrecos(double precoInicial, double precoFinal)
        {

            return this._contexto.Produtos
                .Where(p => p.PrecoVenda >= precoInicial && p.PrecoVenda <= precoFinal)
                .Include(p => p.Categoria)
                .ToList();
        }

        public List<Produto> BuscarProdutosPelaCategoria(int idCategoria)
        {

            return this._contexto.Produtos
                .Where(p => p.CategoriaId == idCategoria)
                .Include(p => p.Categoria)
                .ToList();
        }

        public List<Produto> BuscarTodos()
        {

            return this._contexto.Produtos
                 .Include(p => p.Categoria)
                 .ToList();
        }

        public void Cadastrar(Produto model)
        {
            this._contexto.Produtos.Add(model);
            this._contexto.SaveChanges();
        }

        public void ControlarUnidadesEstoqueProduto(string tipoOperacao, int quantidade, int idProduto)
        {
            Produto produto = this.BuscarPeloId(idProduto);

            if (tipoOperacao.Equals("incremento"))
            {
                produto.QuantidadeUnidadesEstoque += quantidade;
            }
            else
            {
                produto.QuantidadeUnidadesEstoque -= quantidade;
            }

            if (produto.QuantidadeUnidadesEstoque == 0)
            {
                produto.StatusEstoque = "zerado";
            }
            else
            {
                produto.StatusEstoque = "ok";
            }

            this._contexto.Produtos.Entry(produto).State = EntityState.Modified;
            this._contexto.SaveChanges();
        }

        public void Deletar(Produto modelDeletar)
        {
            this._contexto.Produtos.Entry(modelDeletar).State = EntityState.Deleted;
            this._contexto.SaveChanges();
        }

        public void Editar(Produto modelEditar)
        {
            this._contexto.Produtos.Entry(modelEditar).State = EntityState.Modified;
            this._contexto.SaveChanges();
        }

    }
}

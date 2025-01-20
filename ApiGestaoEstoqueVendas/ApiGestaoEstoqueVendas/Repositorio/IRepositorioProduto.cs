namespace ApiGestaoEstoqueVendas.Repositorio
{
    public interface IRepositorioProduto<Produto>: IRepositorioDeletar<Produto>, IRepositorioEditar<Produto>
    {

        List<Produto> BuscarProdutosPelaCategoria(int idCategoria);

        List<Produto> BuscarProdutosEntrePrecos(double precoInicial, double precoFinal);

        void ControlarUnidadesEstoqueProduto(String tipoOperacao, int quantidade, int idProduto);

    }
}

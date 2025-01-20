using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;

namespace ApiGestaoEstoqueVendas.Utils
{
    public static class ConverteProduto
    {

        public static ProdutoDTO ConverterProdutoEmProdutoDTO(Produto produto)
        {
            ProdutoDTO produtoDTO = new ProdutoDTO();
            produtoDTO.ProdutoId = produto.Id;
            produtoDTO.Nome = produto.Nome;
            produtoDTO.QuantidadeUnidadesEstoque = produto.QuantidadeUnidadesEstoque;
            produtoDTO.Ativo = produto.Ativo;
            produtoDTO.Descricao = produto.Descricao;
            produtoDTO.DataCadastro = produto.DataCadastro;
            produtoDTO.PrecoCompra = produto.PrecoCompra;
            produtoDTO.PrecoVenda = produto.PrecoVenda;
            produtoDTO.CategoriaDTO = ConverteCategoria.ConverterCategoriaEmCategoriaDTO(produto.Categoria);

            return produtoDTO;
        }

        public static List<ProdutoDTO> ConverterListaProdutosEmListaProdutosDTO(List<Produto> produtos)
        {
            List<ProdutoDTO> produtoDTOS = new List<ProdutoDTO>();

            foreach (Produto produto in produtos)
            {
                produtoDTOS.Add(ConverteProduto.ConverterProdutoEmProdutoDTO(produto));
            }

            return produtoDTOS;
        }

    }
}

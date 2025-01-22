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
            produtoDTO.StatusEstoque = produto.StatusEstoque;
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

        public static Produto ConverterProdutoDTOCadastrarEditarEmProduto(ProdutoDTOCadastrarEditar produtoDTOCadastrarEditar)
        {
            Produto produto = new Produto();

            produto.Id = produtoDTOCadastrarEditar.ProdutoId;
            produto.Nome = produtoDTOCadastrarEditar.Nome.Trim();
            produto.Descricao = produtoDTOCadastrarEditar.Descricao.Trim();
            produto.QuantidadeUnidadesEstoque = produtoDTOCadastrarEditar.QuantidadeUnidadesEstoque;
            produto.PrecoCompra = produtoDTOCadastrarEditar.PrecoCompra;
            produto.PrecoVenda = produtoDTOCadastrarEditar.PrecoVenda;
            produto.Ativo = produtoDTOCadastrarEditar.Ativo;
            produto.DataCadastro = produtoDTOCadastrarEditar.DataCadastro;
            produto.CategoriaId = produtoDTOCadastrarEditar.CategoriaId;
            produto.StatusEstoque = produtoDTOCadastrarEditar.StatusEstoque;

            return produto;
        }

    }
}

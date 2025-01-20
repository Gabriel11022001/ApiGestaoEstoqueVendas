using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Utils;

namespace ApiGestaoEstoqueVendas.Servico
{
    public interface IProdutoServico
    {

        RespostaHttp<List<ProdutoDTO>> BuscarTodosProdutos();

        RespostaHttp<ProdutoDTO> CadastrarProduto(ProdutoDTOCadastrarEditar produtoDTOCadastrarEditar);

        RespostaHttp<ProdutoDTO> EditarProduto(ProdutoDTOCadastrarEditar produtoDTOCadastrarEditar);

        RespostaHttp<ProdutoDTO> BuscarProdutoPeloId(int idProduto);

        RespostaHttp<Boolean> DeletarProduto(int idProdutoDeletar);

        RespostaHttp<List<ProdutoDTO>> BuscarProdutosEntrePrecos(ProdutoFiltroEntrePrecos filtro);

        RespostaHttp<List<ProdutoDTO>> BuscarProdutosPelaCategoria(int idCategoriaFiltrar);

        RespostaHttp<Boolean> ControlarEstoqueProduto(int idProduto, int unidades, String operacao);

    }
}

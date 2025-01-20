using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Repositorio;
using ApiGestaoEstoqueVendas.Utils;

namespace ApiGestaoEstoqueVendas.Servico
{
    public class ProdutoServico : IProdutoServico
    {

        private IRepositorioProduto<Produto> _produtoRepositorio;

        public ProdutoServico(IRepositorioProduto<Produto> produtoRepositorio)
        {
            this._produtoRepositorio = produtoRepositorio;
        }

        // buscar o produto pelo id na base de dados
        public RespostaHttp<ProdutoDTO> BuscarProdutoPeloId(int idProduto)
        {

            try
            {
                Produto produto = this._produtoRepositorio.BuscarPeloId(idProduto);

                if (produto is null)
                {

                    return new RespostaHttp<ProdutoDTO>()
                    {
                        Mensagem = "Não existe um produto cadastrado com esse id na base de dados!",
                        ConteudoRetorno = null,
                        Ok = true
                    };
                }

                ProdutoDTO produtoDTO = ConverteProduto.ConverterProdutoEmProdutoDTO(produto);

                return new RespostaHttp<ProdutoDTO>()
                {
                    Mensagem = "Produto encontrado com sucesso!",
                    Ok = true,
                    ConteudoRetorno = produtoDTO
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<ProdutoDTO>()
                {
                    Mensagem = "Erro ao tentar-se consultar o produto pelo id!",
                    ConteudoRetorno = null,
                    Ok = false
                };
            }

        }

        public RespostaHttp<List<ProdutoDTO>> BuscarProdutosEntrePrecos(ProdutoFiltroEntrePrecos filtro)
        {
            throw new NotImplementedException();
        }

        public RespostaHttp<List<ProdutoDTO>> BuscarProdutosPelaCategoria(int idCategoriaFiltrar)
        {
            throw new NotImplementedException();
        }

        // buscar todos os produtos
        public RespostaHttp<List<ProdutoDTO>> BuscarTodosProdutos()
        {
            var respostaConsultarTodosProdutos = new RespostaHttp<List<ProdutoDTO>>();

            try
            {
                var produtos = this._produtoRepositorio.BuscarTodos();

                if (produtos.Count == 0)
                {
                    respostaConsultarTodosProdutos.ConteudoRetorno = new List<ProdutoDTO>();
                    respostaConsultarTodosProdutos.Mensagem = "Não existem produtos cadastrados na base de dados!";
                    respostaConsultarTodosProdutos.Ok = true;
                }
                else
                {
                    respostaConsultarTodosProdutos.Mensagem = "Produtos encontrados com sucesso!";
                    respostaConsultarTodosProdutos.Ok = true;
                    respostaConsultarTodosProdutos.ConteudoRetorno = ConverteProduto.ConverterListaProdutosEmListaProdutosDTO(produtos);
                }

            }
            catch (Exception e)
            {
                respostaConsultarTodosProdutos.Mensagem = "Erro ao tentar-se consultar todos os produtos!";
                respostaConsultarTodosProdutos.Ok = false;
                respostaConsultarTodosProdutos.ConteudoRetorno = new List<ProdutoDTO>();
            }

            return respostaConsultarTodosProdutos;
        }

        public RespostaHttp<ProdutoDTO> CadastrarProduto(ProdutoDTOCadastrarEditar produtoDTOCadastrarEditar)
        {
            throw new NotImplementedException();
        }

        public RespostaHttp<bool> ControlarEstoqueProduto(int idProduto, int unidades, string operacao)
        {
            throw new NotImplementedException();
        }

        public RespostaHttp<bool> DeletarProduto(int idProdutoDeletar)
        {
            throw new NotImplementedException();
        }

        public RespostaHttp<ProdutoDTO> EditarProduto(ProdutoDTOCadastrarEditar produtoDTOCadastrarEditar)
        {
            throw new NotImplementedException();
        }

    }
}

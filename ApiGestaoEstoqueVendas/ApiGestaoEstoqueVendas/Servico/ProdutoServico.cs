using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Repositorio;
using ApiGestaoEstoqueVendas.Utils;

namespace ApiGestaoEstoqueVendas.Servico
{
    public class ProdutoServico : IProdutoServico
    {

        private IRepositorioProduto<Produto> _produtoRepositorio;
        private ICategoriaRepositorio<Categoria> _categoriaRepositorio;

        public ProdutoServico(IRepositorioProduto<Produto> produtoRepositorio, ICategoriaRepositorio<Categoria> categoriaRepositorio)
        {
            this._produtoRepositorio = produtoRepositorio;
            this._categoriaRepositorio = categoriaRepositorio;
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

        // buscar os produtos entre preços
        public RespostaHttp<List<ProdutoDTO>> BuscarProdutosEntrePrecos(ProdutoFiltroEntrePrecos filtro)
        {

            try
            {

                if (filtro.PrecoInicialFiltro > filtro.PrecoFinalFiltro)
                {

                    return new RespostaHttp<List<ProdutoDTO>>()
                    {
                        Mensagem = "O preço inicial do filtro deve ser menor ou igual ao preço final!",
                        Ok = false,
                        ConteudoRetorno = null
                    };
                }

                List<Produto> produtos = this._produtoRepositorio.BuscarProdutosEntrePrecos(filtro.PrecoInicialFiltro, filtro.PrecoFinalFiltro);

                if (produtos.Count == 0)
                {

                    return new RespostaHttp<List<ProdutoDTO>>()
                    {
                        Mensagem = "Não existem produtos cadastrados entre esses preços!",
                        Ok = true,
                        ConteudoRetorno = new List<ProdutoDTO>()
                    };
                }

                List<ProdutoDTO> produtosDTO = ConverteProduto.ConverterListaProdutosEmListaProdutosDTO(produtos);

                return new RespostaHttp<List<ProdutoDTO>>()
                {
                    Mensagem = "Produtos encontrados com sucesso!",
                    Ok = true,
                    ConteudoRetorno = produtosDTO
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<List<ProdutoDTO>>()
                {
                    Mensagem = "Erro ao tentar-se consultar os produtos entre preços!",
                    ConteudoRetorno = null,
                    Ok = false
                };
            }

        }

        // buscar produtos pela categoria
        public RespostaHttp<List<ProdutoDTO>> BuscarProdutosPelaCategoria(int idCategoriaFiltrar)
        {
            RespostaHttp<List<ProdutoDTO>> respostaConsultarProdutosPelaCategoria = new RespostaHttp<List<ProdutoDTO>>();

            try
            {
                // validar se existe uma categoria cadastrada na base de dados com esse id
                Categoria categoria = this._categoriaRepositorio.BuscarCategoriaPeloId(idCategoriaFiltrar);

                if (categoria is null)
                {
                    respostaConsultarProdutosPelaCategoria.Mensagem = "Não existe uma categoria cadastrada na base de dados com esse id!";
                    respostaConsultarProdutosPelaCategoria.Ok = false;
                    respostaConsultarProdutosPelaCategoria.ConteudoRetorno = null;
                }
                else
                {
                    List<Produto> produtos = this._produtoRepositorio.BuscarProdutosPelaCategoria(idCategoriaFiltrar);

                    if (produtos.Count > 0)
                    {
                        List<ProdutoDTO> produtosDTO = ConverteProduto.ConverterListaProdutosEmListaProdutosDTO(produtos);

                        respostaConsultarProdutosPelaCategoria.Mensagem = "Produtos encontrados com sucesso!";
                        respostaConsultarProdutosPelaCategoria.Ok = true;
                        respostaConsultarProdutosPelaCategoria.ConteudoRetorno = produtosDTO;
                    }
                    else
                    {
                        respostaConsultarProdutosPelaCategoria.Mensagem = "Não existem produtos cadastrados na base de dados com essa categoria!";
                        respostaConsultarProdutosPelaCategoria.Ok = true;
                        respostaConsultarProdutosPelaCategoria.ConteudoRetorno = new List<ProdutoDTO>();
                    }

                }

            }
            catch (Exception e)
            {
                respostaConsultarProdutosPelaCategoria.Mensagem = "Erro ao tentar-se consultar os produtos pela categoria: " + e.Message;
                respostaConsultarProdutosPelaCategoria.Ok = false;
                respostaConsultarProdutosPelaCategoria.ConteudoRetorno = null;
            }

            return respostaConsultarProdutosPelaCategoria;
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

        // cadastrar produto na base de dados
        public RespostaHttp<ProdutoDTO> CadastrarProduto(ProdutoDTOCadastrarEditar produtoDTOCadastrarEditar)
        {
            RespostaHttp<ProdutoDTO> respostaCadastrarProduto = new RespostaHttp<ProdutoDTO>();

            try
            {

                if (!ValidaUnidadesEstoqueProduto.Validar(produtoDTOCadastrarEditar.QuantidadeUnidadesEstoque))
                {
                    respostaCadastrarProduto.Mensagem = "Quantidade de unidades do produto em estoque inválido!";
                    respostaCadastrarProduto.Ok = false;
                    respostaCadastrarProduto.ConteudoRetorno = null;
                }
                else if (!ValidaPrecosProduto.ValidarPreco(produtoDTOCadastrarEditar.PrecoCompra))
                {
                    respostaCadastrarProduto.Mensagem = "Preço de compra inválido!";
                    respostaCadastrarProduto.Ok = false;
                    respostaCadastrarProduto.ConteudoRetorno = null;
                }
                else if (!ValidaPrecosProduto.ValidarPreco(produtoDTOCadastrarEditar.PrecoVenda))
                {
                    respostaCadastrarProduto.Mensagem = "Preço de venda inválido!";
                    respostaCadastrarProduto.Ok = false;
                    respostaCadastrarProduto.ConteudoRetorno = null;
                }
                else if (!ValidaPrecosProduto.ValidarPrecoCompraMaiorPrecoVenda(produtoDTOCadastrarEditar.PrecoCompra, produtoDTOCadastrarEditar.PrecoVenda))
                {
                    respostaCadastrarProduto.Mensagem = "O preço de compra não deve ser menor que o preço de venda do produto!";
                    respostaCadastrarProduto.Ok = false;
                    respostaCadastrarProduto.ConteudoRetorno = null;
                }
                else if (this.ValidarExisteOutroProdutoCadastradoMesmoNome(produtoDTOCadastrarEditar))
                {
                    respostaCadastrarProduto.Mensagem = "Já existe um produto cadastrado com esse nome!";
                    respostaCadastrarProduto.Ok = false;
                    respostaCadastrarProduto.ConteudoRetorno = null;
                }
                else if (this._categoriaRepositorio.BuscarCategoriaPeloId(produtoDTOCadastrarEditar.CategoriaId) is null)
                {
                    respostaCadastrarProduto.Mensagem = "Não existe uma categoria cadastrada na base de dados com esse id!";
                    respostaCadastrarProduto.Ok = false;
                    respostaCadastrarProduto.ConteudoRetorno = null;
                }
                else
                {
                    // persistir o produto na base de dados
                    produtoDTOCadastrarEditar.DataCadastro = DateTime.Now;

                    if (produtoDTOCadastrarEditar.QuantidadeUnidadesEstoque == 0)
                    {
                        produtoDTOCadastrarEditar.StatusEstoque = "zerado";
                    }
                    else
                    {
                        produtoDTOCadastrarEditar.StatusEstoque = "ok";
                    }

                    Produto produtoCadastrar = ConverteProduto.ConverterProdutoDTOCadastrarEditarEmProduto(produtoDTOCadastrarEditar);
                    this._produtoRepositorio.Cadastrar(produtoCadastrar);

                    respostaCadastrarProduto.Mensagem = "Produto cadastrado com sucesso!";
                    respostaCadastrarProduto.Ok = true;
                    respostaCadastrarProduto.ConteudoRetorno = ConverteProduto.ConverterProdutoEmProdutoDTO(produtoCadastrar);
                }

            }
            catch (Exception e)
            {
                respostaCadastrarProduto.Mensagem = "Erro ao tentar-se cadastrar o produto!";
                respostaCadastrarProduto.Ok = false;
                respostaCadastrarProduto.ConteudoRetorno = null;
            }

            return respostaCadastrarProduto;
        }

        private Boolean ValidarExisteOutroProdutoCadastradoMesmoNome(ProdutoDTOCadastrarEditar produtoDTOCadastrarEditar)
        {
            Produto produtoCadastradoComNomeInformado = this._produtoRepositorio.BuscarProdutoPeloNome(produtoDTOCadastrarEditar.Nome.Trim());

            if (produtoCadastradoComNomeInformado is not null && produtoCadastradoComNomeInformado.Id != produtoDTOCadastrarEditar.ProdutoId)
            {

                return true;
            }

            return false;
        }

        // controlar estoque do produto(incrementar ou decrementar unidades em estoque)
        public RespostaHttp<bool> ControlarEstoqueProduto(int idProduto, int unidades, string operacao)
        {

            try
            {
                // validar a operação
                if (operacao.Trim() != "incremento" && operacao.Trim() != "decremento")
                {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "A operação deve ser 'decremento' ou 'incremento!'",
                        Ok = false,
                        ConteudoRetorno = false
                    };
                }

                // validar a quantidade de unidades
                if (unidades <= 0) {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "Quantidade de unidades inválida!",
                        Ok = false,
                        ConteudoRetorno = false
                    };
                }

                Produto produtoControleEstoque = this._produtoRepositorio.BuscarPeloId(idProduto);

                if (produtoControleEstoque is null)
                {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "Não existe um produto cadastrado na base de dados com o id informado!",
                        Ok = false,
                        ConteudoRetorno = false
                    };
                }

                if (operacao.Equals("decremento") && unidades > produtoControleEstoque.QuantidadeUnidadesEstoque)
                {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "O produto em questão possui " + produtoControleEstoque.QuantidadeUnidadesEstoque + " unidades em estoque, não é possível decrementar " + unidades + " unidades!",
                        Ok = false,
                        ConteudoRetorno = false
                    };
                }

                this._produtoRepositorio.ControlarUnidadesEstoqueProduto(tipoOperacao: operacao, quantidade: unidades, idProduto: idProduto);

                return new RespostaHttp<bool>()
                {
                    Mensagem = "Quantidade de unidades " + operacao + " com sucesso!",
                    ConteudoRetorno = true,
                    Ok = true
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<bool>()
                {
                    Mensagem = "Erro ao tentar-se atualizar o estoque do produto!",
                    Ok = false,
                    ConteudoRetorno = false
                };
            }

        }

        // deletar produto
        public RespostaHttp<bool> DeletarProduto(int idProdutoDeletar)
        {
            RespostaHttp<Boolean> respostaDeletarProduto = new RespostaHttp<bool>();

            try
            {
                // validar se existe um produto cadastrado na base de dados com o id informado
                Produto produtoDeletar = this._produtoRepositorio.BuscarPeloId(idProdutoDeletar);

                if (produtoDeletar == null)
                {
                    respostaDeletarProduto.Mensagem = "Não existe um produto cadastrado na base de dados com o id informado!";
                    respostaDeletarProduto.ConteudoRetorno = false;
                    respostaDeletarProduto.Ok = false;
                }
                else
                {
                    this._produtoRepositorio.Deletar(produtoDeletar);

                    respostaDeletarProduto.Mensagem = "Produto deletado com sucesso!";
                    respostaDeletarProduto.ConteudoRetorno = true;
                    respostaDeletarProduto.Ok = true;
                }

            }
            catch (Exception e)
            {
                respostaDeletarProduto.Mensagem = "Erro ao tentar-se deletar o produto da base de dados!";
                respostaDeletarProduto.Ok = false;
                respostaDeletarProduto.ConteudoRetorno = false;
            }

            return respostaDeletarProduto;
        }

        public RespostaHttp<ProdutoDTO> EditarProduto(ProdutoDTOCadastrarEditar produtoDTOCadastrarEditar)
        {
            throw new NotImplementedException();
        }

    }
}

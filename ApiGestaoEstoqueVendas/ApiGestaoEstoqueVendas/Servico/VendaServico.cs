using ApiGestaoEstoqueVendas.Contexto;
using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Repositorio;
using ApiGestaoEstoqueVendas.Utils;

namespace ApiGestaoEstoqueVendas.Servico
{
    public class VendaServico: IVendaServico
    {

        private ApiGestaoEstoqueVendasAppDbContexto _contexto;
        private IVendaRepositorio<Venda> _vendaRepositorio;
        private IRepositorioProduto<Produto> _produtoRepositorio;

        public VendaServico(
            IVendaRepositorio<Venda> vendaRepositorio,
            IRepositorioProduto<Produto> produtoRepositorio,
            ApiGestaoEstoqueVendasAppDbContexto contexto
        )
        {
            this._vendaRepositorio = vendaRepositorio;
            this._produtoRepositorio = produtoRepositorio;
            this._contexto = contexto;
        }

        // buscar todas as vendas
        public RespostaHttp<List<VendaDTO>> BuscarTodasVendas()
        {

            try
            {
                var vendas = this._vendaRepositorio.BuscarTodos();

                if (vendas.Count == 0)
                {

                    return new RespostaHttp<List<VendaDTO>>()
                    {
                        Mensagem = "Não existem vendas cadastradas na base de dados!",
                        ConteudoRetorno = new List<VendaDTO>(),
                        Ok = true
                    };
                }

                List<VendaDTO> vendasDTO = new List<VendaDTO>();

                foreach (Venda venda in vendas)
                {
                    VendaDTO vendaDTO = new VendaDTO();
                    vendaDTO.DataVenda = venda.DataVenda;
                    vendaDTO.VendaId = venda.Id;
                    vendaDTO.ValorTotalVenda = venda.ValorTotalVenda;
                    vendaDTO.Status = venda.Status;

                    List<ItemVendaDTO> itensVendaDTO = new List<ItemVendaDTO>();

                    venda.ItensVenda.ForEach(itemVenda =>
                    {
                        ItemVendaDTO itemVendaDTO = new ItemVendaDTO();
                        itemVendaDTO.ItemVendaId = itemVenda.Id;
                        itemVendaDTO.DataRegistroItemVenda = itemVenda.DataRegistroItemVenda;
                        itemVendaDTO.QuantidadeUnidadesProdutoItem = itemVenda.QuantidadeUnidadesProdutoItem;
                        itemVendaDTO.ProdutoId = itemVenda.ProdutoId;
                        itemVendaDTO.PrecoProdutoMomentoVenda = itemVenda.PrecoProdutoMomentoVenda;

                        Produto produto = this._produtoRepositorio.BuscarPeloId(itemVenda.ProdutoId);

                        itemVendaDTO.ProdutoDTO = new ProdutoDTO()
                        {
                            ProdutoId = produto.Id,
                            Nome = produto.Nome,
                            Descricao = produto.Descricao,
                            PrecoCompra = produto.PrecoCompra,
                            PrecoVenda = produto.PrecoVenda,
                            Ativo = produto.Ativo,
                            DataCadastro = produto.DataCadastro,
                            StatusEstoque = produto.StatusEstoque,
                            QuantidadeUnidadesEstoque = produto.QuantidadeUnidadesEstoque,
                            CategoriaDTO = new CategoriaDTO()
                            {
                                CategoriaId = produto.CategoriaId,
                                Nome = produto.Categoria.Nome,
                                Ativo = produto.Categoria.Ativo
                            }
                        };

                        itensVendaDTO.Add(itemVendaDTO);
                    });

                    vendaDTO.ItensVendaDTO = itensVendaDTO;

                    vendasDTO.Add(vendaDTO);
                }

                return new RespostaHttp<List<VendaDTO>>()
                {
                    Mensagem = "Vendas listadas com sucesso!",
                    Ok = true,
                    ConteudoRetorno = vendasDTO
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<List<VendaDTO>>()
                {
                    Mensagem = "Erro ao tentar-se consultar todas as vendas!",
                    ConteudoRetorno = new List<VendaDTO>(),
                    Ok = false
                };
            }

        }

        // realizar a venda
        public RespostaHttp<VendaDTO> RealizarVenda(VendaDTOCadastrarEditar vendaDTOCadastrarEditar)
        {
            this._contexto.Database.BeginTransaction();

            try
            {
                // validar os itens da venda
                Boolean itensValidos = true;
                String mensagemErroItensInvalidos = "";

                foreach (ItemVendaDTOCadastrarEditar itemVendaDTOCadastrarEditar in vendaDTOCadastrarEditar.ItensVendaCadastrarEditarDTO)
                {
                    // validar se o produto existe na base de dados ou não
                    Produto produto = this._produtoRepositorio.BuscarPeloId(itemVendaDTOCadastrarEditar.ProdutoId);

                    if (produto is null)
                    {
                        itensValidos = false;
                        mensagemErroItensInvalidos = "Não existe o produto cadastrado com o id " + itemVendaDTOCadastrarEditar.ProdutoId;
                    }
                    else if (produto.QuantidadeUnidadesEstoque < itemVendaDTOCadastrarEditar.QuantidadeUnidadesProdutoItem)
                    {
                        itensValidos = false;
                        mensagemErroItensInvalidos = "O produto " + produto.Nome + " só possui " + produto.QuantidadeUnidadesEstoque + " unidades em estoque!";
                    }
                    else if (itemVendaDTOCadastrarEditar.PrecoProdutoMomentoVenda <= 0)
                    {
                        itensValidos = false;
                        mensagemErroItensInvalidos = "O preço informado do produto no momento da venda é inválido!";
                    }
                    else if (!produto.Ativo)
                    {
                        itensValidos = false;
                        mensagemErroItensInvalidos = "O produto " + produto.Nome + " não está ativo!";
                    }

                }

                if (!itensValidos)
                {

                    return new RespostaHttp<VendaDTO>()
                    {
                        Mensagem = mensagemErroItensInvalidos,
                        ConteudoRetorno = null,
                        Ok = false
                    };
                }

                // registrar a venda na base de dados
                Venda venda = new Venda();
                venda.DataVenda = DateTime.Now;
                venda.Status = "andamento";
                venda.ValorTotalVenda = 0;

                this._vendaRepositorio.Cadastrar(venda);

                // registrar os itens da venda
                vendaDTOCadastrarEditar.ItensVendaCadastrarEditarDTO.ForEach(itemVendaCadastarEditarDTO =>
                {
                    ItemVenda itemVenda = new ItemVenda();
                    itemVenda.VendaId = venda.Id;
                    itemVenda.QuantidadeUnidadesProdutoItem = itemVendaCadastarEditarDTO.QuantidadeUnidadesProdutoItem;
                    itemVenda.ValorItem = itemVendaCadastarEditarDTO.QuantidadeUnidadesProdutoItem * itemVendaCadastarEditarDTO.PrecoProdutoMomentoVenda;
                    itemVenda.PrecoProdutoMomentoVenda = itemVendaCadastarEditarDTO.PrecoProdutoMomentoVenda;
                    itemVenda.ProdutoId = itemVendaCadastarEditarDTO.ProdutoId;
                    itemVenda.DataRegistroItemVenda = DateTime.Now;

                    // registrar o item da venda na base de dados
                    this._vendaRepositorio.CadastrarItemVenda(itemVenda);
                });

                // atualizar os estoques dos produtos
                vendaDTOCadastrarEditar.ItensVendaCadastrarEditarDTO.ForEach(ItemVendaDTOCadastrarEditar =>
                {
                    Produto produtoAtualizarEstoque = this._produtoRepositorio.BuscarPeloId(ItemVendaDTOCadastrarEditar.ProdutoId);
                    this._produtoRepositorio.ControlarUnidadesEstoqueProduto("decremento", ItemVendaDTOCadastrarEditar.QuantidadeUnidadesProdutoItem, ItemVendaDTOCadastrarEditar.ProdutoId);
                });

                venda.ValorTotalVenda = CalculaValorTotalVenda.CalcularValorTotalVenda(vendaDTOCadastrarEditar.ItensVendaCadastrarEditarDTO);
                venda.Status = "aguardando pagamento";

                this._vendaRepositorio.Editar(venda);

                VendaDTO vendaDTO = new VendaDTO();
                vendaDTO.VendaId = venda.Id;
                vendaDTO.DataVenda = venda.DataVenda;
                vendaDTO.ValorTotalVenda = venda.ValorTotalVenda;
                vendaDTO.Status = venda.Status;

                List<ItemVendaDTO> itensVendaDTO = new List<ItemVendaDTO>();

                foreach (ItemVenda itemVenda in venda.ItensVenda)
                {
                    ItemVendaDTO itemVendaDTO = new ItemVendaDTO();
                    itemVendaDTO.ItemVendaId = itemVenda.Id;
                    itemVendaDTO.DataRegistroItemVenda = itemVenda.DataRegistroItemVenda;
                    itemVendaDTO.ValorItem = itemVenda.ValorItem;
                    itemVendaDTO.QuantidadeUnidadesProdutoItem = itemVenda.QuantidadeUnidadesProdutoItem;
                    itemVendaDTO.ProdutoId = itemVenda.ProdutoId;
                    itemVendaDTO.PrecoProdutoMomentoVenda = itemVenda.PrecoProdutoMomentoVenda;

                    ProdutoDTO produtoDTO = new ProdutoDTO();
                    produtoDTO.ProdutoId = itemVenda.Produto.Id;
                    produtoDTO.Nome = itemVenda.Produto.Nome;
                    produtoDTO.Ativo = itemVenda.Produto.Ativo;
                    produtoDTO.PrecoCompra = itemVenda.Produto.PrecoCompra;
                    produtoDTO.PrecoVenda = itemVenda.Produto.PrecoVenda;
                    produtoDTO.QuantidadeUnidadesEstoque = itemVenda.Produto.QuantidadeUnidadesEstoque;
                    produtoDTO.DataCadastro = itemVenda.Produto.DataCadastro;
                    produtoDTO.Descricao = itemVenda.Produto.Descricao;

                    itemVendaDTO.ProdutoDTO = produtoDTO;

                    itensVendaDTO.Add(itemVendaDTO);
                }

                vendaDTO.ItensVendaDTO = itensVendaDTO;

                this._contexto.Database.CommitTransaction();

                return new RespostaHttp<VendaDTO>()
                {
                    Mensagem = "Venda efetuada com sucesso!",
                    Ok = true,
                    ConteudoRetorno = vendaDTO
                };
            }
            catch (Exception e)
            {
                this._contexto.Database.RollbackTransaction();

                return new RespostaHttp<VendaDTO>()
                {
                    Mensagem = "Erro ao tentar-se realizar a venda!",
                    ConteudoRetorno = null,
                    Ok = false
                };
            }

        }

        // buscar venda pelo id
        public RespostaHttp<VendaDTO> BuscarVendaPeloId(int idVendaConsultar)
        {
            RespostaHttp<VendaDTO> respostaConsultarVenda = new RespostaHttp<VendaDTO>();

            try
            {
                Venda venda = this._vendaRepositorio.BuscarPeloId(idVendaConsultar);

                if (venda is null)
                {
                    respostaConsultarVenda.Ok = false;
                    respostaConsultarVenda.ConteudoRetorno = null;
                    respostaConsultarVenda.Mensagem = "Não existe uma venda cadastrada na base de dados com esse id!";
                }
                else
                {
                    VendaDTO vendaDTO = new VendaDTO();
                    List<ItemVendaDTO> itensVendaDTO = new List<ItemVendaDTO>();

                    foreach (ItemVenda itemVenda in venda.ItensVenda)
                    {
                        ItemVendaDTO itemVendaDTO = new ItemVendaDTO();
                        itemVendaDTO.ItemVendaId = itemVenda.Id;
                        itemVendaDTO.ValorItem = itemVenda.ValorItem;
                        itemVendaDTO.QuantidadeUnidadesProdutoItem = itemVenda.QuantidadeUnidadesProdutoItem;
                        itemVendaDTO.PrecoProdutoMomentoVenda = itemVenda.PrecoProdutoMomentoVenda;
                        itemVendaDTO.ProdutoId = itemVenda.ProdutoId;
                        itemVendaDTO.DataRegistroItemVenda = itemVenda.DataRegistroItemVenda;

                        Produto produto = this._produtoRepositorio.BuscarPeloId(itemVenda.ProdutoId);
                        itemVendaDTO.ProdutoDTO = new ProdutoDTO()
                        {
                            ProdutoId = produto.Id,
                            DataCadastro = produto.DataCadastro,
                            Nome = produto.Nome,
                            Descricao = produto.Descricao,
                            PrecoCompra = produto.PrecoCompra,
                            PrecoVenda = produto.PrecoVenda,
                            QuantidadeUnidadesEstoque = produto.QuantidadeUnidadesEstoque,
                            Ativo = produto.Ativo,
                            StatusEstoque = produto.StatusEstoque
                        };

                        itensVendaDTO.Add(itemVendaDTO);
                    }

                    vendaDTO.ItensVendaDTO = itensVendaDTO;
                    vendaDTO.VendaId = venda.Id;
                    vendaDTO.DataVenda = venda.DataVenda;
                    vendaDTO.Status = venda.Status;
                    vendaDTO.ValorTotalVenda = venda.ValorTotalVenda;

                    respostaConsultarVenda.Ok = true;
                    respostaConsultarVenda.Mensagem = "Venda encontrada com sucesso!";
                    respostaConsultarVenda.ConteudoRetorno = vendaDTO;
                }

            }
            catch (Exception e)
            {
                respostaConsultarVenda.Mensagem = "Erro ao tentar-se consultar a venda pelo id!";
                respostaConsultarVenda.ConteudoRetorno = null;
                respostaConsultarVenda.Ok = false;
            }

            return respostaConsultarVenda;
        }

        // buscar vendas entre periodo
        public RespostaHttp<List<VendaDTO>> BuscarVendasEntrePeriodo(VendaDTOFiltroPeriodos vendaDTOFiltroPeriodos)
        {
            RespostaHttp<List<VendaDTO>> respostaConsultarVendasPeriodo = new RespostaHttp<List<VendaDTO>>();

            return respostaConsultarVendasPeriodo;
        }

        // deletar venda
        public RespostaHttp<bool> DeletarVenda(int idVendaDeletar)
        {
            this._contexto.Database.BeginTransaction();

            try
            {
                Venda vendaDeletar = this._vendaRepositorio.BuscarPeloId(idVendaDeletar);

                if (vendaDeletar is null)
                {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "Não existe uma venda cadastrada com esse id!",
                        ConteudoRetorno = false,
                        Ok = false
                    };
                }

                List<ItemVenda> itensVenda = vendaDeletar.ItensVenda.ToList();

                // deletar os itens da venda
                foreach (ItemVenda itemVenda in itensVenda)
                {
                    this._vendaRepositorio.DeletarItemVenda(itemVenda);
                }

                this._vendaRepositorio.DeletarVenda(vendaDeletar);

                this._contexto.Database.CommitTransaction();

                return new RespostaHttp<bool>()
                {
                    Mensagem = "Venda deletada com sucesso!",
                    ConteudoRetorno = true,
                    Ok = true
                };
            }
            catch (Exception e)
            {
                this._contexto.Database.RollbackTransaction();

                return new RespostaHttp<bool>()
                {
                    Mensagem = "Erro ao tentar-se deletar a venda: " + e.Message,
                    Ok = false,
                    ConteudoRetorno = false
                };
            }

        }

    }
}

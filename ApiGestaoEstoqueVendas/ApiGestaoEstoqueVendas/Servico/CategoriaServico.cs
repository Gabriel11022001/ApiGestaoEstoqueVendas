using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Repositorio;
using ApiGestaoEstoqueVendas.Utils;

namespace ApiGestaoEstoqueVendas.Servico
{
    public class CategoriaServico : ICategoriaServico
    {

        private ICategoriaRepositorio<Categoria> _categoriaRepositorio;
        private IRepositorioProduto<Produto> _produtoRepositorio;

        public CategoriaServico(ICategoriaRepositorio<Categoria> categoriaRepositorio, IRepositorioProduto<Produto> produtoRepositorio)
        {
            this._categoriaRepositorio = categoriaRepositorio;
            this._produtoRepositorio = produtoRepositorio;
        }

        // alterar o status da categoria
        public RespostaHttp<bool> AlterarStatusCategoria(int idCategoriaAlterarStatus, bool novoStatus)
        {

            try
            {
                // validar se a categoria existe na base de dados
                Categoria categoria = this._categoriaRepositorio.BuscarCategoriaPeloId(idCategoriaAlterarStatus);

                if (categoria is null)
                {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "Não existe uma categoria cadastrada com esse id na base de dados!",
                        ConteudoRetorno = false,
                        Ok = false
                    };
                }

                if (categoria.Ativo == novoStatus)
                {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "A categoria " + categoria.Nome + " já possui status " + (novoStatus ? "ativo" : "inativo") + "!",
                        ConteudoRetorno = novoStatus,
                        Ok = true
                    };
                }

                this._categoriaRepositorio.AlterarStatusCategoria(idCategoriaAlterarStatus, novoStatus);

                return new RespostaHttp<bool>()
                {
                    Mensagem = "Status da categoria " + categoria.Nome + " foi alterado com sucesso!",
                    ConteudoRetorno = novoStatus,
                    Ok = true
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<bool>()
                {
                    Mensagem = "Erro ao tentar-se alterar o status da categoria!",
                    Ok = false,
                    ConteudoRetorno = false
                };
            }

        }

        // buscar categoria pelo id
        public RespostaHttp<CategoriaDTO> BuscarCategoriaPeloId(int idCategoria)
        {

            try
            {
                Categoria categoria = this._categoriaRepositorio.BuscarCategoriaPeloId(idCategoria);

                if (categoria is null)
                {

                    return new RespostaHttp<CategoriaDTO>()
                    {
                        Mensagem = "Não existe uma categoria cadastrada com esse id!",
                        ConteudoRetorno = null,
                        Ok = true
                    };
                }

                return new RespostaHttp<CategoriaDTO>()
                {
                    Mensagem = "Categoria encontrada com sucesso!",
                    ConteudoRetorno = ConverteCategoria.ConverterCategoriaEmCategoriaDTO(categoria),
                    Ok = true
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<CategoriaDTO>()
                {
                    Mensagem = "Erro ao tentar-se consultar a categoria pelo id!",
                    Ok = false,
                    ConteudoRetorno = null
                };
            }

        }

        // buscar todas as categorias
        public RespostaHttp<List<CategoriaDTO>> BuscarCategorias()
        {

            try
            {
                List<Categoria> categorias = this._categoriaRepositorio.BuscarTodos();

                if (categorias.Count == 0)
                {

                    return new RespostaHttp<List<CategoriaDTO>>()
                    {
                        Mensagem = "Não existem categorias cadastradas na base de dados!",
                        Ok = true,
                        ConteudoRetorno = new List<CategoriaDTO>()
                    };
                }

                List<CategoriaDTO> categoriasDTO = ConverteCategoria.ConverterListaCategoriasEmListaCategoriasDTO(categorias);

                return new RespostaHttp<List<CategoriaDTO>>()
                {
                    Mensagem = "Categorias listadas com sucesso!",
                    Ok = true,
                    ConteudoRetorno = categoriasDTO
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<List<CategoriaDTO>>()
                {
                    Mensagem = "Erro ao tentar-se consultar as categorias!",
                    ConteudoRetorno = null,
                    Ok = false
                };
            }

        }

        public RespostaHttp<List<CategoriaDTO>> BuscarCategoriasPeloStatus(bool ativo)
        {
            throw new NotImplementedException();
        }

        // cadastrar categoria na base de dados
        public RespostaHttp<CategoriaDTO> CadastrarCategoria(CategoriaDTO categoriaDTO)
        {

            try
            {
                // validar se já existe outra categoria cadastrada com o mesmo nome
                if (this._categoriaRepositorio.BuscarCategoriaPeloNome(categoriaDTO.Nome) is not null)
                {

                    return new RespostaHttp<CategoriaDTO>()
                    {
                        Mensagem = "Já existe outra categoria cadastrada com esse nome!",
                        Ok = false,
                        ConteudoRetorno = null
                    };
                }

                Categoria categoriaCadastrar = ConverteCategoria.ConverterCategoriaDTOEmCategoria(categoriaDTO);
                this._categoriaRepositorio.Cadastrar(categoriaCadastrar);

                categoriaDTO.CategoriaId = categoriaCadastrar.Id;

                return new RespostaHttp<CategoriaDTO>()
                {
                    Mensagem = "Categoria cadastrada com sucesso!",
                    Ok = true,
                    ConteudoRetorno = categoriaDTO
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<CategoriaDTO>()
                {
                    Mensagem = "Erro ao tentar-se cadastrar a categoria na base de dados!",
                    Ok = false,
                    ConteudoRetorno = null
                };
            }

        }

        // deletar categoria
        public RespostaHttp<bool> DeletarCategoria(int categoriaId)
        {

            try
            {
                // validar se a categoria está cadastrada na base de dados
                Categoria categoriaDeletar = this._categoriaRepositorio.BuscarCategoriaPeloId(categoriaId);

                if (categoriaDeletar is null)
                {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "Não existe uma categoria cadastrada na base de dados com esse id!",
                        Ok = false,
                        ConteudoRetorno = false
                    };
                }

                // validar se a categoria está relacionada a algum produto
                List<Produto> produtosCategoria = this._produtoRepositorio.BuscarProdutosPelaCategoria(categoriaId);

                if (produtosCategoria.Count > 0)
                {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "Essa categoria está relacionada a um ou mais produtos!",
                        ConteudoRetorno = false,
                        Ok = false
                    };
                }

                this._categoriaRepositorio.Deletar(categoriaDeletar);

                return new RespostaHttp<bool>() { Mensagem = "Categoria deletada com sucesso!", Ok = true, ConteudoRetorno = true };
            }
            catch (Exception e)
            {

                return new RespostaHttp<bool>()
                {
                    Mensagem = "Erro ao tentar-se deletar a categoria na base de dados!",
                    Ok = false,
                    ConteudoRetorno = false
                };
            }

        }

        public RespostaHttp<CategoriaDTO> EditarCategoria(CategoriaDTO categoriaDTO)
        {
            throw new NotImplementedException();
        }

    }
}

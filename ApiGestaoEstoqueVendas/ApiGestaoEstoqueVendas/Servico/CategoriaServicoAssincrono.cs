using ApiGestaoEstoqueVendas.Contexto;
using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Repositorio;
using ApiGestaoEstoqueVendas.Utils;

namespace ApiGestaoEstoqueVendas.Servico
{
    public class CategoriaServicoAssincrono : ICategoriaServicoAssincrono
    {

        private ICategoriaRepositorioAssincrono _categoriaRepositorioAssincrono;
        private CategoriaRepositorioEspecializada _categoriaRepositorioEspecializado;

        public CategoriaServicoAssincrono(ICategoriaRepositorioAssincrono categoriaRepositorioAssincrono, ApiGestaoEstoqueVendasAppDbContexto contexto)
        {
            this._categoriaRepositorioAssincrono = categoriaRepositorioAssincrono;
            this._categoriaRepositorioEspecializado = new CategoriaRepositorioEspecializada(contexto);
        }

        // buscar categoria pelo id de forma assincrona
        public async Task<RespostaHttp<CategoriaDTO>> BuscarCategoriaPeloIdAssincrono(int idCategoriaConsultar)
        {

            try
            {
                Categoria categoria = await this._categoriaRepositorioAssincrono.BuscarCategoriaPeloIdAssincrono(idCategoriaConsultar);

                if (categoria is null)
                {

                    return new RespostaHttp<CategoriaDTO>()
                    {
                        Ok = false,
                        Mensagem = "Não existe uma categoria cadastrada com esse id na base de dados!",
                        ConteudoRetorno = null
                    };
                }

                CategoriaDTO categoriaDTO = new CategoriaDTO();
                categoriaDTO.CategoriaId = categoria.Id;
                categoriaDTO.Nome = categoria.Nome;
                categoriaDTO.Ativo = categoria.Ativo;

                return new RespostaHttp<CategoriaDTO>()
                {
                    Mensagem = "Categoria encontrada com sucesso!",
                    ConteudoRetorno = categoriaDTO,
                    Ok = true
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<CategoriaDTO>()
                {
                    Mensagem = "Erro ao tentar-se consultar a categoria pelo id!",
                    ConteudoRetorno = null,
                    Ok = false
                };
            }

        }

        // buscar categoria pelo id teste exception personalizada
        public async Task<RespostaHttp<CategoriaDTO>> BuscarCategoriaPeloIdAssincronoTesteException(int idCategoria)
        {

            try
            {
                Categoria categoria = await this._categoriaRepositorioEspecializado.BuscarPeloId(idCategoria);

                CategoriaDTO categoriaDTO = new CategoriaDTO();
                categoriaDTO.CategoriaId = categoria.Id;
                categoriaDTO.Nome = categoria.Nome;
                categoriaDTO.Ativo = categoria.Ativo;

                return new RespostaHttp<CategoriaDTO>()
                {
                    Mensagem = "Categoria encontrada com sucesso!",
                    Ok = true,
                    ConteudoRetorno = categoriaDTO
                };
            }
            catch (CategoriaNaoExisteException categoriaNaoExisteException)
            {

                return new RespostaHttp<CategoriaDTO>() { Mensagem = "Não existe uma categoria cadastrada com esse id na base de dados!", ConteudoRetorno = null, Ok = false };
            }
            catch (Exception e)
            {

                return new RespostaHttp<CategoriaDTO>()
                {
                    Mensagem = "Erro ao tentar-se consultar a categoria pelo id!",
                    ConteudoRetorno = null,
                    Ok = false
                };
            }

        }

        public async Task<RespostaHttp<RetornoListagemPaginadaDTO<CategoriaDTO>>> BuscarCategoriasAssincrono(int paginaAtual, int elementosPorPagina)
        {
            throw new NotImplementedException();
        }

        // buscar categorias filtradas
        public async Task<RespostaHttp<RetornoListagemPaginadaDTO<CategoriaDTO>>> BuscarCategoriasFiltroAssincrono(FiltroCategoriasDTO filtroCategoriasDTO)
        {
            RespostaHttp<RetornoListagemPaginadaDTO<CategoriaDTO>> respostaConsultarCategoriasFiltro = new RespostaHttp<RetornoListagemPaginadaDTO<CategoriaDTO>>();
            RetornoListagemPaginadaDTO<CategoriaDTO> retornoListagemPaginadaCategorias = new RetornoListagemPaginadaDTO<CategoriaDTO>();

            try
            {

            }
            catch (Exception e)
            {
                respostaConsultarCategoriasFiltro.ConteudoRetorno = null;
                respostaConsultarCategoriasFiltro.Mensagem = "Erro ao tentar-se filtrar as categorias!";
                respostaConsultarCategoriasFiltro.Ok = false;
            }

            return respostaConsultarCategoriasFiltro;
        }

        // cadastrar categoria de forma assincrona
        public async Task<RespostaHttp<CategoriaDTO>> CadastrarCategoriaAssincrono(CategoriaDTO categoriaDTO)
        {
            RespostaHttp<CategoriaDTO> respostaCadastrarCategoria = new RespostaHttp<CategoriaDTO>();

            try
            {
                // validar se já existe outra categoria cadastrada com o mesmo nome
                Categoria categoriaCadastradaMesmoNome = await this._categoriaRepositorioAssincrono.BuscarCategoriaPeloNome(categoriaDTO.Nome);

                if (categoriaCadastradaMesmoNome is not null)
                {
                    respostaCadastrarCategoria.Mensagem = "Já existe uma categoria cadastrada com o mesmo nome no banco de dados!";
                    respostaCadastrarCategoria.Ok = false;
                    respostaCadastrarCategoria.ConteudoRetorno = null;
                }
                else
                {
                    Categoria categoriaCadastrar = new Categoria();
                    categoriaCadastrar.Nome = categoriaDTO.Nome;
                    categoriaCadastrar.Ativo = categoriaDTO.Ativo;

                    await this._categoriaRepositorioAssincrono.CadastrarCategoriaAssincrono(categoriaCadastrar);

                    categoriaDTO.CategoriaId = categoriaCadastrar.Id;

                    respostaCadastrarCategoria.Mensagem = "Categoria cadastrada com sucesso!";
                    respostaCadastrarCategoria.Ok = true;
                    respostaCadastrarCategoria.ConteudoRetorno = categoriaDTO;
                }

            }
            catch (Exception e)
            {
                respostaCadastrarCategoria.Ok = false;
                respostaCadastrarCategoria.Mensagem = "Erro ao tentar-se cadastrar a categoria!";
                respostaCadastrarCategoria.ConteudoRetorno = null;
            }

            return respostaCadastrarCategoria;
        }

        // deletar categoria assincrono
        public async Task<RespostaHttp<bool>> DeletarCategoriaAssincrono(int idCategoriaDeletar)
        {

            try
            {
                Categoria categoriaDeletar = await this._categoriaRepositorioAssincrono.BuscarCategoriaPeloIdAssincrono(idCategoriaDeletar);

                if (categoriaDeletar is null)
                {

                    return new RespostaHttp<bool>()
                    {
                        Mensagem = "Não existe uma categoria cadastrada na base de dados com esse id!",
                        ConteudoRetorno = false,
                        Ok = false
                    };
                }

                await this._categoriaRepositorioAssincrono.DeletarCategoriaAssincrono(categoriaDeletar);

                return new RespostaHttp<bool>()
                {
                    Mensagem = "Categoria deletada com sucesso!",
                    ConteudoRetorno = true,
                    Ok = true
                };
            }
            catch (Exception e)
            {

                return new RespostaHttp<bool>()
                {
                    Mensagem = "Erro ao tentar-se deletar a categoria!",
                    ConteudoRetorno = false,
                    Ok = false
                };
            }

        }

        public async Task<RespostaHttp<CategoriaDTO>> EditarCategoriaAssincrono(CategoriaDTO categoriaDTO)
        {
            throw new NotImplementedException();
        }

    }
}

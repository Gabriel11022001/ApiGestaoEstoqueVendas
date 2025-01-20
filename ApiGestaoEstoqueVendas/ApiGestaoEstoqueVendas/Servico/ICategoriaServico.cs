using ApiGestaoEstoqueVendas.DTO;

namespace ApiGestaoEstoqueVendas.Servico
{
    public interface ICategoriaServico
    {

        RespostaHttp<CategoriaDTO> CadastrarCategoria(CategoriaDTO categoriaDTO);

        RespostaHttp<CategoriaDTO> EditarCategoria(CategoriaDTO categoriaDTO);

        RespostaHttp<List<CategoriaDTO>> BuscarCategorias();

        RespostaHttp<List<CategoriaDTO>> BuscarCategoriasPeloStatus(bool ativo);

        RespostaHttp<CategoriaDTO> BuscarCategoriaPeloId(int idCategoria);

        RespostaHttp<Boolean> DeletarCategoria(int categoriaId);

        RespostaHttp<Boolean> AlterarStatusCategoria(int idCategoriaAlterarStatus, bool novoStatus);

    }
}

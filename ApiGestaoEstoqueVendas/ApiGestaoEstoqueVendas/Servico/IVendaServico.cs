using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Repositorio;

namespace ApiGestaoEstoqueVendas.Servico
{
    public interface IVendaServico
    {

        RespostaHttp<VendaDTO> RealizarVenda(VendaDTOCadastrarEditar vendaDTOCadastrarEditar);

        RespostaHttp<List<VendaDTO>> BuscarTodasVendas();

        RespostaHttp<VendaDTO> BuscarVendaPeloId(int idVendaConsultar);

        RespostaHttp<List<VendaDTO>> BuscarVendasEntrePeriodo(VendaDTOFiltroPeriodos vendaDTOFiltroPeriodos);

        RespostaHttp<Boolean> DeletarVenda(int idVendaDeletar);

    }
}

using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestaoEstoqueVendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendaController : ControllerBase
    {

        private IVendaServico _vendaServico;

        public VendaController(IVendaServico vendaServico)
        {
            this._vendaServico = vendaServico;
        }

        // realizar venda
        [ HttpPost ]
        public IActionResult RealizarVenda(VendaDTOCadastrarEditar vendaDTOCadastrarEditar)
        {
            var respostaRealizarVenda = this._vendaServico.RealizarVenda(vendaDTOCadastrarEditar);

            if (respostaRealizarVenda.Ok)
            {

                return Ok(respostaRealizarVenda);
            }

            return BadRequest(respostaRealizarVenda);
        }

        // buscar todas as vendas
        [ HttpGet ]
        public IActionResult BuscarTodasVendas()
        {
            var respostaConsultarTodasVendas = this._vendaServico.BuscarTodasVendas();

            if (respostaConsultarTodasVendas.Ok)
            {

                return Ok(respostaConsultarTodasVendas);
            }

            return BadRequest(respostaConsultarTodasVendas);
        }

        // buscar venda pelo id
        [ HttpGet("{idVenda:int}") ]
        public IActionResult BuscarVendaPeloId(int idVenda)
        {
            RespostaHttp<VendaDTO> respostaConsultarVendaPeloId = this._vendaServico.BuscarVendaPeloId(idVenda);

            if (respostaConsultarVendaPeloId.Ok)
            {

                return Ok(respostaConsultarVendaPeloId);
            }

            return BadRequest(respostaConsultarVendaPeloId);
        }

        // buscar as vendas realizadas entre um período
        [ HttpGet("buscar-vendas-entre-periodos") ]
        public IActionResult BuscarVendasEntrePeriodo(VendaDTOFiltroPeriodos vendaDTOFiltroPeriodos)
        {
            RespostaHttp<List<VendaDTO>> respostaConsultarVendasEntrePeriodo = this._vendaServico.BuscarVendasEntrePeriodo(vendaDTOFiltroPeriodos);

            return respostaConsultarVendasEntrePeriodo.Ok ? Ok(respostaConsultarVendasEntrePeriodo) : BadRequest(respostaConsultarVendasEntrePeriodo);
        }

        // deletar venda
        [ HttpDelete("{idVendaDeletar:int}") ]
        public IActionResult DeletarVenda(int idVendaDeletar)
        {
            var respostaDeletarVenda = this._vendaServico.DeletarVenda(idVendaDeletar);

            if (respostaDeletarVenda.Ok)
            {

                return Ok(respostaDeletarVenda);
            }

            return BadRequest(respostaDeletarVenda);
        }

    }
}

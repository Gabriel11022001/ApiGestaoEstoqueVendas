using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestaoEstoqueVendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {

        private IProdutoServico _produtoServico;

        public ProdutoController(IProdutoServico produtoServico)
        {
            this._produtoServico = produtoServico;
        }

        // buscar produto pelo id
        [ HttpGet("{id:int}") ]
        public IActionResult BuscarProdutoPeloId(int id)
        {
            RespostaHttp<ProdutoDTO> respostaConsultarProdutoPeloId = this._produtoServico.BuscarProdutoPeloId(id);

            return respostaConsultarProdutoPeloId.Ok ? Ok(respostaConsultarProdutoPeloId) : BadRequest(respostaConsultarProdutoPeloId);
        }

        // buscar todos os produtos
        [ HttpGet ]
        public IActionResult BuscarTodosProdutos()
        {
            var respostaConsultarTodosProdutos = this._produtoServico.BuscarTodosProdutos();

            if (respostaConsultarTodosProdutos.Ok)
            {

                return Ok(respostaConsultarTodosProdutos);
            }

            return BadRequest(respostaConsultarTodosProdutos);
        }

    }
}

using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Servico;
using ApiGestaoEstoqueVendas.Utils;
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

        // buscar produtos pela categoria
        [ HttpGet("buscar-pela-categoria/{idCategoria:int}") ]
        public IActionResult BuscarProdutosPelaCategoria(int idCategoria)
        {
            RespostaHttp<List<ProdutoDTO>> respostaBuscarProdutosPelaCategoria = this._produtoServico.BuscarProdutosPelaCategoria(idCategoria);

            if (respostaBuscarProdutosPelaCategoria.Ok)
            {

                return Ok(respostaBuscarProdutosPelaCategoria);
            }

            return BadRequest(respostaBuscarProdutosPelaCategoria);
        }

        // cadastrar produto na base de dados
        [ HttpPost ]
        public IActionResult CadastrarProduto(ProdutoDTOCadastrarEditar produtoDTOCadastrarEditar)
        {
            var respostaCadastrarProduto = this._produtoServico.CadastrarProduto(produtoDTOCadastrarEditar);
       
            if (respostaCadastrarProduto.Ok)
            {

                return Ok(respostaCadastrarProduto);
            }

            return BadRequest(respostaCadastrarProduto);
        }

        // buscar produtos entre preços
        [ HttpGet("buscar-entre-precos") ]
        public IActionResult BuscarProdutosEntrePrecos([ FromQuery ] double precoInicial, double precoFinal)
        {
            var filtro = new ProdutoFiltroEntrePrecos();
            filtro.PrecoInicialFiltro = precoInicial;
            filtro.PrecoFinalFiltro = precoFinal;

            var respostaConsultarProdutosEntrePrecos = this._produtoServico.BuscarProdutosEntrePrecos(filtro);

            if (respostaConsultarProdutosEntrePrecos.Ok)
            {

                return Ok(respostaConsultarProdutosEntrePrecos);
            }

            return BadRequest(respostaConsultarProdutosEntrePrecos);
        }

    }
}

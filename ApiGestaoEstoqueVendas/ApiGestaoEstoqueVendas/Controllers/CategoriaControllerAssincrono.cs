using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestaoEstoqueVendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaControllerAssincrono : ControllerBase
    {

        private ICategoriaServicoAssincrono _categoriaServicoAssincrono;

        public CategoriaControllerAssincrono(ICategoriaServicoAssincrono categoriaServicoAssincrono)
        {
            this._categoriaServicoAssincrono = categoriaServicoAssincrono;
        }

        // buscar categoria pelo id assincrono
        [ HttpGet("{idCategoria:int}") ]
        public async Task<IActionResult> BuscarCategoriaPeloIdAssincrono(int idCategoria)
        {
            RespostaHttp<CategoriaDTO> respostaConsultarCategoriaPeloId = await this._categoriaServicoAssincrono.BuscarCategoriaPeloIdAssincrono(idCategoria);

            if (respostaConsultarCategoriaPeloId.Ok)
            {

                return Ok(respostaConsultarCategoriaPeloId);
            }

            if (respostaConsultarCategoriaPeloId.Mensagem.Equals("Não existe uma categoria cadastrada com esse id na base de dados!"))
            {

                return NotFound(respostaConsultarCategoriaPeloId);
            }

            return BadRequest(respostaConsultarCategoriaPeloId);
        }

        // cadastrar categoria de forma assincrona
        [ HttpPost ]
        public async Task<IActionResult> CadastrarCategoriaAssincrono(CategoriaDTO categoriaDTOCadastrar)
        {
            RespostaHttp<CategoriaDTO> respostaCadastrarCategoria = await this._categoriaServicoAssincrono.CadastrarCategoriaAssincrono(categoriaDTOCadastrar);

            return respostaCadastrarCategoria.Ok ? Ok(respostaCadastrarCategoria) : BadRequest(respostaCadastrarCategoria);
        }

        // deletar categoria assincrono
        [ HttpDelete("{idCategoriaDeletar:int}") ]
        public async Task<IActionResult> DeletarCategoriaAssincrono(int idCategoriaDeletar)
        {
            RespostaHttp<Boolean> respostaDeletarCategoria = await this._categoriaServicoAssincrono.DeletarCategoriaAssincrono(idCategoriaDeletar);

            return respostaDeletarCategoria.Ok ? Ok(respostaDeletarCategoria) : BadRequest(respostaDeletarCategoria);
        }

        // buscar categoria pelo id teste exception
        [ HttpGet("buscar-pelo-id-teste-exception/{idCategoria:int}") ]
        public async Task<IActionResult> BuscarCategoriaPeloIdTesteException(int idCategoria)
        {
            RespostaHttp<CategoriaDTO> resposta = await this._categoriaServicoAssincrono.BuscarCategoriaPeloIdAssincronoTesteException(idCategoria);

            if (resposta.Ok)
            {

                return Ok(resposta);
            }

            return BadRequest(resposta);
        }

    }
}

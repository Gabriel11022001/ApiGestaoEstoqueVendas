using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Servico;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiGestaoEstoqueVendas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {

        private ICategoriaServico _categoriaServico;

        public CategoriaController(ICategoriaServico categoriaServico)
        {
            this._categoriaServico = categoriaServico;
        }

        // cadastrar categoria na base de dados
        [ HttpPost ]
        public IActionResult CadastrarCategoria(CategoriaDTO categoriaDTO)
        {
            RespostaHttp<CategoriaDTO> repostaCadastrarCategoria = this._categoriaServico.CadastrarCategoria(categoriaDTO);

            if (repostaCadastrarCategoria.Ok)
            {

                return Ok(repostaCadastrarCategoria);
            }

            return BadRequest(repostaCadastrarCategoria);
        }

        // buscar todas as categorias
        [ HttpGet ]
        public IActionResult BuscarCategorias()
        {
            RespostaHttp<List<CategoriaDTO>> respostaConsultarCategorias = this._categoriaServico.BuscarCategorias();

            if (respostaConsultarCategorias.Ok)
            {

                return Ok(respostaConsultarCategorias);
            }

            return BadRequest(respostaConsultarCategorias);
        }

        // buscar categoria pelo id
        [ HttpGet("{idCategoria:int}") ]
        public IActionResult BuscarCategoriaPeloId(int idCategoria)
        {
            RespostaHttp<CategoriaDTO> respostaConsultarCategoria = this._categoriaServico.BuscarCategoriaPeloId(idCategoria);

            if (respostaConsultarCategoria.Ok)
            {

                return Ok(respostaConsultarCategoria);
            }

            return BadRequest(respostaConsultarCategoria);
        }

        // alterar o status da categoria
        [ HttpPut("alterar-status-categoria") ]
        public IActionResult AlterarStatusCategoria([ FromQuery ] int idCategoria, bool novoStatus)
        {
            RespostaHttp<Boolean> respostaAlterarStatusCategoria = this._categoriaServico.AlterarStatusCategoria(idCategoria, novoStatus);

            if (respostaAlterarStatusCategoria.Ok)
            {

                return Ok(respostaAlterarStatusCategoria);
            }

            return BadRequest(respostaAlterarStatusCategoria);
        }

        // deletar categoria
        [ HttpDelete("{idCategoriaDeletar:int}") ]
        public IActionResult DeletarCategoria(int idCategoriaDeletar)
        {
            RespostaHttp<Boolean> respostaDeletarCategoria = this._categoriaServico.DeletarCategoria(idCategoriaDeletar);

            if (respostaDeletarCategoria.Ok)
            {

                return Ok(respostaDeletarCategoria);
            }

            return BadRequest(respostaDeletarCategoria);
        }

    }
}

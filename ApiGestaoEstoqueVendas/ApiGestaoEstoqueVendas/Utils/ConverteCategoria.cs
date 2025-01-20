using ApiGestaoEstoqueVendas.DTO;
using ApiGestaoEstoqueVendas.Model;

namespace ApiGestaoEstoqueVendas.Utils
{
    public static class ConverteCategoria
    {

        public static CategoriaDTO ConverterCategoriaEmCategoriaDTO(Categoria categoria)
        {
            CategoriaDTO categoriaDTO = new CategoriaDTO();
            categoriaDTO.CategoriaId = categoria.Id;
            categoriaDTO.Nome = categoria.Nome;
            categoriaDTO.Ativo = categoria.Ativo;

            return categoriaDTO;
        }

        public static Categoria ConverterCategoriaDTOEmCategoria(CategoriaDTO categoriaDTO)
        {

            return new Categoria()
            {
                Id = categoriaDTO.CategoriaId,
                Nome = categoriaDTO.Nome,
                Ativo = categoriaDTO.Ativo
            };
        }

        public static List<CategoriaDTO> ConverterListaCategoriasEmListaCategoriasDTO(List<Categoria> categorias)
        {
            List<CategoriaDTO> categoriasDTO = new List<CategoriaDTO>();

            foreach (Categoria categoria in categorias)
            {
                categoriasDTO.Add(ConverterCategoriaEmCategoriaDTO(categoria));
            }

            return categoriasDTO;
        }

    }
}

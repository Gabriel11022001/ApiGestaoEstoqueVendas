using System.ComponentModel.DataAnnotations;

namespace ApiGestaoEstoqueVendas.DTO
{
    public class CategoriaDTO
    {

        public int CategoriaId { get; set; }
        [ Required(ErrorMessage = "Informe o nome da categoria!") ]
        [ StringLength(150, MinimumLength = 3, ErrorMessage = "O nome da categoria deve ter entre 3 e 150 caracteres!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe se a categoria está ativa ou não!") ]
        public Boolean Ativo { get; set; }

    }
}

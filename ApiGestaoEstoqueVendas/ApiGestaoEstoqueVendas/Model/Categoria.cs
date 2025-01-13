using System.ComponentModel.DataAnnotations;

namespace ApiGestaoEstoqueVendas.Model
{
    public class Categoria
    {
        [ Required(ErrorMessage = "Informe o id da categoria!") ]
        public int Id { get; set; }
        [ Required(ErrorMessage = "Informe o nome da categoria!") ]
        [ MaxLength(150, ErrorMessage = "O nome da categoria deve possuir no máximo 150 caracteres!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe se a categoria está ativa ou não!") ]
        public Boolean Ativo { get; set; }

    }
}

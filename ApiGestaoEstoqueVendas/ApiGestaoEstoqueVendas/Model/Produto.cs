using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiGestaoEstoqueVendas.Model
{
    public class Produto
    {
        [ Required(ErrorMessage = "Informe o id do produto!") ]
        public int Id { get; set; }
        [ Required(ErrorMessage = "Informe o nome do produto!") ]
        [ MaxLength(255, ErrorMessage = "O nome do produto pode possuir no máximo 255 caracteres!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe a descrição do produto!") ]
        public String Descricao { get; set; }
        [ Required(ErrorMessage = "Informe o preço de compra do produto!") ]
        public Double PrecoCompra { get; set; }
        [ Required(ErrorMessage = "Informe o preço de venda do produto!") ]
        public Double PrecoVenda { get; set; }
        [ Required(ErrorMessage = "Informe se o produto está ativo ou não!") ]
        public Boolean Ativo { get; set; }
        [ Required(ErrorMessage = "Informe o status do produto em estoque!") ]
        public String StatusEstoque { get; set; }
        [ Required(ErrorMessage = "Informe a quantidade de unidades do produto em estoque!") ]  
        public int QuantidadeUnidadesEstoque { get; set; }
        [ Required(ErrorMessage = "Informe a data de cadastro do produto!") ]
        public DateTime DataCadastro { get; set; }
        [ Required(ErrorMessage = "Informe o id da categoria do produto!") ]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

    }
}

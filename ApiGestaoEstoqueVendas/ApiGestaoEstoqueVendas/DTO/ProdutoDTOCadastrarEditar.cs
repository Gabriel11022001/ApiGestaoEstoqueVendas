using System.ComponentModel.DataAnnotations;

namespace ApiGestaoEstoqueVendas.DTO
{
    public class ProdutoDTOCadastrarEditar
    {
        public int ProdutoId { get; set; }
        [ Required(ErrorMessage = "Informe o nome do produto!") ]
        [ StringLength(150, MinimumLength = 3, ErrorMessage = "O nome do produto deve ter entre 3 e 150 caracteres!") ]
        public String Nome { get; set; }
        [ Required(ErrorMessage = "Informe a descrição do produto!") ]
        public String Descricao { get; set; }
        [ Required(ErrorMessage = "Informe a quantidade de unidades do produto em estoque!") ]
        public int QuantidadeUnidadesEstoque { get; set; }
        [ Required(ErrorMessage = "Informe se o produto está ativo ou não!") ]
        public Boolean Ativo { get; set; }
        public String StatusEstoque { get; set; }
        [ Required(ErrorMessage = "Informe o preço de compra do produto!") ]
        public Double PrecoCompra { get; set; }
        [ Required(ErrorMessage = "Informe o preço de venda do produto!") ]
        public Double PrecoVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        [ Required(ErrorMessage = "Informe a categoria do produto!") ]
        public int CategoriaId { get; set; }
    }
}

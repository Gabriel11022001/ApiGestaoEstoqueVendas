using System.ComponentModel.DataAnnotations;

namespace ApiGestaoEstoqueVendas.Model
{
    public class ItemVenda
    {
        public int Id { get; set; }
        [ Required(ErrorMessage = "Informe a data de registro do item da venda!") ]
        public DateTime DataRegistroItemVenda { get; set; }
        [ Required(ErrorMessage = "Informe o preço do produto no momento de relizar a venda!") ]
        public Double PrecoProdutoMomentoVenda { get; set; }
        [ Required(ErrorMessage = "Informe o valor do item!") ]
        public Double ValorItem { get; set; }
        [ Required(ErrorMessage = "Informe a quantidade de unidades do produto no item!") ]
        public int QuantidadeUnidadesProdutoItem { get; set; }
        [ Required(ErrorMessage = "Informe o id do produto!") ]
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        [ Required(ErrorMessage = "Informe o id da venda!") ]
        public int VendaId { get; set; }
        public Venda Venda { get; set; }
    }
}

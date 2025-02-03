using System.ComponentModel.DataAnnotations;

namespace ApiGestaoEstoqueVendas.Model
{
    public class Venda
    {
        public int Id { get; set; }
        [ Required(ErrorMessage = "Informe a data de relização da venda!") ]
        public DateTime DataVenda { get; set; }
        public Double ValorTotalVenda { get; set; }
        [ Required(ErrorMessage = "Informe o status da venda!") ]
        public String Status { get; set; }
        public List<ItemVenda> ItensVenda { get; set; }

        public Venda()
        {
            this.ItensVenda = new List<ItemVenda>();
        }

    }
}

namespace ApiGestaoEstoqueVendas.DTO
{
    public class VendaDTOCadastrarEditar
    {
        public int VendaId { get; set; }
        public DateTime DataVenda { get; set; }
        public Double ValorTotalVenda { get; set; }
        public String Status { get; set; }
        public List<ItemVendaDTOCadastrarEditar> ItensVendaCadastrarEditarDTO { get; set; }

        public VendaDTOCadastrarEditar()
        {
            this.ItensVendaCadastrarEditarDTO = new List<ItemVendaDTOCadastrarEditar>();
        }

    }
}

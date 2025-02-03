namespace ApiGestaoEstoqueVendas.DTO
{
    public class VendaDTO
    {
        public int VendaId { get; set; }
        public DateTime DataVenda { get; set; }
        public Double ValorTotalVenda { get; set; }
        public String Status { get; set; }
        public List<ItemVendaDTO> ItensVendaDTO { get; set; }

        public VendaDTO()
        {
            this.ItensVendaDTO = new List<ItemVendaDTO>();
        }

    }
}

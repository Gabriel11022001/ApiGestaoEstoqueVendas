namespace ApiGestaoEstoqueVendas.DTO
{
    public class ItemVendaDTO
    {
        public int ItemVendaId { get; set; }
        public DateTime DataRegistroItemVenda { get; set; }
        public Double PrecoProdutoMomentoVenda { get; set; }
        public Double ValorItem { get; set; }
        public int QuantidadeUnidadesProdutoItem { get; set; }
        public int ProdutoId { get; set; }
        public ProdutoDTO ProdutoDTO { get; set; }
    }
}

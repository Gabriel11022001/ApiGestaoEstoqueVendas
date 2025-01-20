namespace ApiGestaoEstoqueVendas.DTO
{
    public class ProdutoDTO
    {
        public int ProdutoId { get; set; }
        public String Nome { get; set; }
        public String Descricao { get; set; }
        public int QuantidadeUnidadesEstoque { get; set; }
        public Boolean Ativo { get; set; }
        public String StatusEstoque { get; set; }
        public Double PrecoCompra { get; set; }
        public Double PrecoVenda { get; set; }
        public DateTime DataCadastro { get; set; }
        public CategoriaDTO CategoriaDTO { get; set; }
    }
}

namespace ApiGestaoEstoqueVendas.DTO
{
    public class RetornoListagemPaginadaDTO<T>
    {

        public int PaginaAtual { get; set; }
        public int TotalPaginas { get; set; }
        public List<T> Itens { get; set; }

    }
}

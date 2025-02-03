using ApiGestaoEstoqueVendas.DTO;

namespace ApiGestaoEstoqueVendas.Utils
{
    public static class CalculaValorTotalVenda
    {

        public static Double CalcularValorTotalVenda(List<ItemVendaDTOCadastrarEditar> itensVendaDTOCadastrarEditar)
        {
            Double valorTotal = 0;

            foreach (ItemVendaDTOCadastrarEditar itemVendaDTOCadastrarEditar in itensVendaDTOCadastrarEditar)
            {
                valorTotal += itemVendaDTOCadastrarEditar.QuantidadeUnidadesProdutoItem * itemVendaDTOCadastrarEditar.PrecoProdutoMomentoVenda;
            }

            return valorTotal;
        }

    }
}

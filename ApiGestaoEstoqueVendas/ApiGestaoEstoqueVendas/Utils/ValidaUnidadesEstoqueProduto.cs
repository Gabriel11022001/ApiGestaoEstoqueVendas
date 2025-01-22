namespace ApiGestaoEstoqueVendas.Utils
{
    public static class ValidaUnidadesEstoqueProduto
    {

        public static Boolean Validar(int quantidadeUnidadesEstoque)
        {

            return quantidadeUnidadesEstoque >= 0;
        }

    }
}

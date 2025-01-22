namespace ApiGestaoEstoqueVendas.Utils
{
    public static class ValidaPrecosProduto
    {

        public static Boolean ValidarPreco(Double precoValidar)
        {

            if (precoValidar <= 0)
            {

                return false;
            }

            return true;
        }

        public static Boolean ValidarPrecoCompraMaiorPrecoVenda(Double precoCompra, Double precoVenda)
        {

            if (precoCompra > precoVenda)
            {

                return false;
            }

            return true;
        }

    }
}

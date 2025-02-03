namespace ApiGestaoEstoqueVendas.Utils
{
    public class CategoriaNaoExisteException: Exception
    {

        public CategoriaNaoExisteException(): base("Categoria não encontrada!") { }

    }
}

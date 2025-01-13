namespace ApiGestaoEstoqueVendas.Repositorio
{
    public interface IRepositorio<T>
    {

        void Cadastrar(T model);

        List<T> BuscarTodos();

        T BuscarPeloId(int id);

    }
}

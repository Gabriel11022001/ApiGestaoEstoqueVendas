namespace ApiGestaoEstoqueVendas.Repositorio
{
    public interface IRepositorioEditar<T>: IRepositorio<T>
    {

        void Editar(T modelEditar);

    }
}

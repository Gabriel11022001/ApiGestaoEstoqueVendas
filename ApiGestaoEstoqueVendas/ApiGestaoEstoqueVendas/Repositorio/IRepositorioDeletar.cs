namespace ApiGestaoEstoqueVendas.Repositorio
{
    public interface IRepositorioDeletar<T>: IRepositorio<T>
    {

        void Deletar(T modelDeletar);

    }
}

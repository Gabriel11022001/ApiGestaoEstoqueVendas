using ApiGestaoEstoqueVendas.Utils;

namespace ApiGestaoEstoqueVendas.Repositorio
{
    public abstract class RepositorioBase<T>
    {

        public abstract Task<List<T>> BuscarTodos();

        public abstract Task Cadastrar(T model);

        public abstract Task Editar(T model);

        public abstract Task<T> BuscarPeloId(int id);

        public abstract Task Deletar(T modelDeletar);

    }
}

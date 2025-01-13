using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Repositorio;

namespace ApiGestaoEstoqueVendas.Contexto
{
    public static class InjecaoDependencia
    {

        // injeções de dependência dos repositórios
        public static void ImplementarInjecoesDependenciaRepositorios(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoriaRepositorio<Categoria>, CategoriaRepositorio>();
        }

        // injeções de dependência dos services
        public static void ImplementarInjecoesDependenciaServicos(WebApplicationBuilder builder)
        {

        }

    }
}

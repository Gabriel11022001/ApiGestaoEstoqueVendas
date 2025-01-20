using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Repositorio;
using ApiGestaoEstoqueVendas.Servico;

namespace ApiGestaoEstoqueVendas.Contexto
{
    public static class InjecaoDependencia
    {

        // injeções de dependência dos repositórios
        public static void ImplementarInjecoesDependenciaRepositorios(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoriaRepositorio<Categoria>, CategoriaRepositorio>();
            builder.Services.AddScoped<IRepositorioProduto<Produto>, ProdutoRepositorio>();
        }

        // injeções de dependência dos services
        public static void ImplementarInjecoesDependenciaServicos(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoriaServico, CategoriaServico>();
            builder.Services.AddScoped<IProdutoServico, ProdutoServico>();
        }

    }
}

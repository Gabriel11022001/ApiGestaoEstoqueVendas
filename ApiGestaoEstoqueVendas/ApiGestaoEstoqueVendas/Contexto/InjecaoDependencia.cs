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
            builder.Services.AddScoped<IVendaRepositorio<Venda>, VendaRepositorio>();
            builder.Services.AddScoped<ICategoriaRepositorioAssincrono, CategoriaRepositorioAssincrono>();
        }

        // injeções de dependência dos services
        public static void ImplementarInjecoesDependenciaServicos(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<ICategoriaServico, CategoriaServico>();
            builder.Services.AddScoped<IProdutoServico, ProdutoServico>();
            builder.Services.AddScoped<IVendaServico, VendaServico>();
            builder.Services.AddScoped<ICategoriaServicoAssincrono, CategoriaServicoAssincrono>();
        }

    }
}

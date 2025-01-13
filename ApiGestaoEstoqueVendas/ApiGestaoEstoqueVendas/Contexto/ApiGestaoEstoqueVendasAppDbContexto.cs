using ApiGestaoEstoqueVendas.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoEstoqueVendas.Contexto
{
    public class ApiGestaoEstoqueVendasAppDbContexto: DbContext
    {

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }

        public ApiGestaoEstoqueVendasAppDbContexto(DbContextOptions<ApiGestaoEstoqueVendasAppDbContexto> options): base(options)
        {

        }

    }
}

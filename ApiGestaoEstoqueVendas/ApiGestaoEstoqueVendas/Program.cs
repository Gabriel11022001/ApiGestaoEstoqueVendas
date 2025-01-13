using ApiGestaoEstoqueVendas.Contexto;
using ApiGestaoEstoqueVendas.Model;
using ApiGestaoEstoqueVendas.Repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// configurar o contexto do banco de dados
var stringConexaoBancoDados = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiGestaoEstoqueVendasAppDbContexto>(opcoes =>
{
    opcoes.UseSqlServer(stringConexaoBancoDados);
});

// implementar as inje��es de depend�ncia do projeto
// inje��o de depend�ncia dos reposit�rios
InjecaoDependencia.ImplementarInjecoesDependenciaRepositorios(builder);

// inje��o de depend�ncia dos services

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

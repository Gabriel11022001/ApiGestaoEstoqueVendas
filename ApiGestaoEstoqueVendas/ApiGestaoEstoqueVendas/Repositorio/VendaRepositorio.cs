using ApiGestaoEstoqueVendas.Contexto;
using ApiGestaoEstoqueVendas.Model;
using Microsoft.EntityFrameworkCore;

namespace ApiGestaoEstoqueVendas.Repositorio
{
    public class VendaRepositorio : IVendaRepositorio<Venda>
    {

        private ApiGestaoEstoqueVendasAppDbContexto _contexto;

        public VendaRepositorio(ApiGestaoEstoqueVendasAppDbContexto contexto)
        {
            this._contexto = contexto;
        }

        public Boolean AlterarStatusVenda(int idVenda, string novoStatus)
        {
            throw new NotImplementedException();
        }

        // buscar venda pelo id
        public Venda BuscarPeloId(int id)
        {

            return this._contexto
                .Vendas
                .Include(v => v.ItensVenda)
                .FirstOrDefault(v => v.Id == id);
        }

        // buscar todas as vendas
        public List<Venda> BuscarTodos()
        {

            return this._contexto.Vendas.Include(v => v.ItensVenda).ToList();
        }

        public List<Venda> BuscarVendasPorPeriodo(DateTime dataInicial, DateTime dataFinal)
        {
            throw new NotImplementedException();
        }

        // buscar vendas pelo status
        public List<Venda> BuscarVendasPorStatus(string status)
        {

            return this._contexto
                .Vendas
                .Where(v => v.Status.Equals(status.ToLower()))
                .Include(v => v.ItensVenda)
                .ToList();
        }

        // cadastrar venda
        public void Cadastrar(Venda model)
        {
            this._contexto.Vendas.Add(model);
            this._contexto.SaveChanges();
        }

        // cadastrar o item da venda na base de dados
        public void CadastrarItemVenda(ItemVenda itemVenda)
        {
            this._contexto.ItensVendas.Add(itemVenda);
            this._contexto.SaveChanges();
        }

        // deletar item da venda
        public void DeletarItemVenda(ItemVenda itemVendaDeletar)
        {
            this._contexto.ItensVendas.Entry(itemVendaDeletar).State = EntityState.Deleted;
            this._contexto.SaveChanges();
        }

        // deletar venda
        public void DeletarVenda(Venda vendaDeletar)
        {
            this._contexto.Vendas.Entry(vendaDeletar).State = EntityState.Deleted;
            this._contexto.SaveChanges();
        }

        // editar venda
        public void Editar(Venda modelEditar)
        {
            this._contexto.Vendas.Entry(modelEditar).State = EntityState.Modified;
            this._contexto.SaveChanges();
        }

    }
}

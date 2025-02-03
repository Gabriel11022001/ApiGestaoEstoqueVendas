using ApiGestaoEstoqueVendas.Model;

namespace ApiGestaoEstoqueVendas.Repositorio
{
    public interface IVendaRepositorio<Venda>: IRepositorio<Venda>, IRepositorioEditar<Venda>
    {

        List<Venda> BuscarVendasPorPeriodo(DateTime dataInicial, DateTime dataFinal);

        List<Venda> BuscarVendasPorStatus(String status);

        Boolean AlterarStatusVenda(int idVenda, String novoStatus);

        void CadastrarItemVenda(ItemVenda itemVenda);

        void DeletarVenda(Venda vendaDeletar);

        void DeletarItemVenda(ItemVenda itemVendaDeletar);

    }
}

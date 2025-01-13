using Microsoft.IdentityModel.Tokens;

namespace ApiGestaoEstoqueVendas.Servico
{
    public class RespostaHttp<T>
    {

        private string _mensagem;
        public String Mensagem
        {
            get
            {

                return this._mensagem.IsNullOrEmpty() ? "Não foi informado uma mensagem para o retorno!" : this._mensagem;
            }
            set 
            {
                
                if (value.Trim().IsNullOrEmpty())
                {
                    this._mensagem = "Não foi informado uma mensagem para o retorno!";
                }
                else
                {
                    this._mensagem = value.Trim();
                }

            }
        }
        public Boolean Ok { get; set; }
        public T ConteudoRetorno { get; set; }

    }
}

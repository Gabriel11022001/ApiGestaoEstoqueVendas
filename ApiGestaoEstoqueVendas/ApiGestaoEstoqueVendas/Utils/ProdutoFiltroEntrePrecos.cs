namespace ApiGestaoEstoqueVendas.Utils
{
    public class ProdutoFiltroEntrePrecos
    {

        private Double _precoInicialFiltro;
        public Double PrecoInicialFiltro
        {
            get
            {

                return this._precoInicialFiltro;
            }
            set
            {

                if (value <= 0)
                {
                    this._precoInicialFiltro = 1;
                }
                else
                {
                    this._precoInicialFiltro = value;
                }

            }
        }
        private Double _precoFinalFiltro;
        public Double PrecoFinalFiltro
        {
            get
            {

                return this._precoFinalFiltro;
            }
            set
            {

                if (value <= 0 || value < this.PrecoInicialFiltro)
                {
                    this._precoFinalFiltro = this.PrecoInicialFiltro;
                }
                else
                {
                    this.PrecoFinalFiltro = value;
                }

            }
        }

    }
}

namespace ControlProduct.Models.ViewModel
{
    public class ProdutoExtraViewModel
    {
        public ProdutoExtraViewModel(PedidoExtra prod)
        {
            Nome = prod.Nome;
            Valor = prod.Valor;
        }

        public string Nome { get; set; }
        public double Valor { get; set; }
    }
}

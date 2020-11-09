namespace ControlProduct.Models.ViewModel
{
    public class ProdutoPedidoViewModel
    {
        public ProdutoPedidoViewModel(Produto prod, int quantidade)
        {
            Nome = prod.Nome;
            Valor = prod.Valor * quantidade;
            Quantidade = quantidade;
        }

        public string Nome { get; set; }
        public double Valor { get; set; }
        public int Quantidade { get; set; }
    }
}

namespace ControlProduct.Models
{
    public class PedidoProduto
    {
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }
        public int Quantidade { get; set; }

        public Pedido Pedido { get; set; }
        public Produto Produto { get; set; }
    }
}

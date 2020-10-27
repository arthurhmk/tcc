namespace ControlProduct.Models
{
    public class PedidoProduto
    {
        public int IdPedido { get; set; }
        public int IdProduto { get; set; }

        public Pedido Pedidos { get; set; }
        public Produto Produto { get; set; }
    }
}

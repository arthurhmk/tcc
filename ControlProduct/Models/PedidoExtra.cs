namespace ControlProduct.Models
{
    public class PedidoExtra
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }

        public Pedido Pedido { get; set; }
    }
}

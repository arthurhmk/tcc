using System;

namespace ControlProduct.Models
{
    public class Pagamento
    {
        public int Id { get; set; }
        public int IdPedido { get; set; }
        public DateTime DataPagamento { get; set; }
        public double Valor { get; set; }

        public Pedido Pedido { get; set; }
    }
}

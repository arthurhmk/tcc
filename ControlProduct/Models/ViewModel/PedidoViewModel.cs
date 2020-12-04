using ControlProduct.Models.Enum;
using System.Collections.Generic;
using System.Linq;

namespace ControlProduct.Models.ViewModel
{
    public class PedidoViewModel
    {
        public PedidoViewModel(Pedido pedido)
        {
            Id = pedido.Id;
            Cliente = pedido.Cliente.Nome;
            Valor = pedido.Valor;
            Pedido = pedido.PedidoProdutos.Select(p => new ProdutoPedidoViewModel(p.Produto, p.Quantidade)).ToList();
            DataPedido = pedido.DataPedido.ToString("yyyy-MM-dd");
            DataEntrega = pedido.DataEntrega.ToString("yyyy-MM-dd");
            LocalEntrega = pedido.EnderecoEntrega?? "A retirar";
            Status = EstadoEnumToString(pedido.Estado);
            StatusEnum = pedido.Estado;
            ValorPago = pedido.Pagamentos.Aggregate(0D, (acc, x) => acc + x.Valor);
        }

        public int Id { get; set; }
        public string Cliente { get; set; }
        public List<ProdutoPedidoViewModel> Pedido { get; set; }
        public double Valor { get; set; }
        public string DataPedido { get; set; }
        public string DataEntrega { get; set; }

        public string LocalEntrega { get; set; }
        public string Status { get; set; }
        public EstadoPedido StatusEnum { get; set; }
        public double ValorPago { get; set; }

        private string EstadoEnumToString(EstadoPedido estado) => estado switch
        {
            EstadoPedido.COMPLETO => "Entregue",
            EstadoPedido.PENDENTE => "Pagamento parcial",
            EstadoPedido.PAGO     => "Pago",
            _ => "NO_STATE"
        };
    }
}

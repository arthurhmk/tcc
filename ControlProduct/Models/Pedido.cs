using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using ControlProduct.Models.Enum;
using System;

namespace ControlProduct.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public double Valor { get; set; }

        public EstadoPedido Estado { get; set; }

        public DateTime DataPedido{ get; set; }

        public DateTime? DataEntrega { get; set; }

        public TipoEntrega Tipo { get; set; }

        public string EnderecoEntrega { get; set; }

        public double ValorEntrega { get; set; }

        public Cliente Cliente { get; set; }

        public List<PedidoProduto> PedidoProdutos { get; set; }
        public List<Pagamento> Pagamentos { get; set; }
        public List<PedidoExtra> Extras { get; set; }
    }
}

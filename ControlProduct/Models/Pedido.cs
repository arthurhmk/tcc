using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;
using ControlProduct.Models.Enum;
using System;

namespace ControlProduct.Models
{
    public class Pedido
    {
        public Pedido()
        {
        }

        public int Id { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [DisplayName("Nome do Cliente")]
        public int IdCliente { get; set; }

        public double Valor { get; set; }

        public EstadoPedido Estado { get; set; }

        public DateTime DataPedido{ get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [DisplayName("Data de Entrega")]
        public DateTime? DataEntrega { get; set; }

        [Required(ErrorMessage = "Esse campo é obrigatório.")]
        [DisplayName("Forma de recebimento do pedido")]
        public TipoEntrega TipoEntrega { get; set; }

        [DisplayName("Endereço para entregar")]
        public string EnderecoEntrega { get; set; }

        [DisplayName("Custo adicional da entrega")]
        public double ValorEntrega { get; set; }

        public Cliente Cliente { get; set; }

        public List<PedidoProduto> PedidoProdutos { get; set; }
        public List<Pagamento> Pagamentos { get; set; }
        public List<PedidoExtra> Extras { get; set; }
    }
}

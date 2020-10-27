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

        public int ClienteId { get; set; }

        public double Valor { get; set; }

        public EstadoProduto Estado { get; set; }

        public DateTime Inicio { get; set; }

        public DateTime Entrega { get; set; }

        public TipoEntrega Tipo { get; set; }

        public string EnderecoEntrega { get; set; }

        public double ValorEntrega { get; set; }

        public Cliente Cliente { get; set; }

        public List<PedidoProduto> PedidoProdutos { get; set; }
        public List<Pagamento> Pagamento { get; set; }
    }
}

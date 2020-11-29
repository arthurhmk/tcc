using System;

namespace ControlProduct.Models
{
    public class Debito
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public decimal Entrada { get; set; }
        public decimal Saida { get; set; }
        public decimal Valor { get; set; }
    }
}

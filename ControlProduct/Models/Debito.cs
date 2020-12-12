using System;

namespace ControlProduct.Models
{
    public class Debito
    {
        public Debito()
        {
            Data = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime Data { get; set; }
        public double Entrada { get; set; }
        public double Saida { get; set; }
        public double Valor { get; set; }
    }
}

using System;

namespace ControlProduct.Models.ViewModel
{
    public class DebitoViewModel
    {
        public DebitoViewModel(Debito deb)
        {
            Id = deb.Id;
            Data = deb.Data.ToString("yyyy/MM");
            Entrada = deb.Entrada;
            Saida = deb.Saida;
            Valor = deb.Valor;
            Total = ((Entrada - Saida) < 0?"- ":"") + "R$" +Math.Abs(Entrada - Saida).ToString("F");
        }


        public int Id { get; set; }
        public string Data { get; set; }
        public double Entrada { get; set; }
        public double Saida { get; set; }
        public double Valor { get; set; }
        public string Total { get; set; }
    }
}

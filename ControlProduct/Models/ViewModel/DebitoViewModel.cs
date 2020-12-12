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
            Total = ((Entrada - Saida) < 0?"- ":"") + "R$" +Math.Abs(Entrada - Saida).ToString();
        }


        public int Id { get; set; }
        public string Data { get; set; }
        public decimal Entrada { get; set; }
        public decimal Saida { get; set; }
        public decimal Valor { get; set; }
        public string Total { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlProduct.Models
{
    public class Cliente
    {
        public int Id { get; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
    }
}

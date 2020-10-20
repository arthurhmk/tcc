using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace ControlProduct.Models
{
    public class Cliente
    {
        public int Id { get; }

        [Required(ErrorMessage ="Esse campo é obrigatório.")]
        [DisplayName("Nome do Cliente")]
        public string Nome { get; set; }

        [DisplayName("Email do Cliente")]
        public string Email { get; set; }

        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [DisplayName("Endereço")]
        public string Endereco { get; set; }
    }
}

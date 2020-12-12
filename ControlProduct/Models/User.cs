using System;
using System.ComponentModel.DataAnnotations;

namespace ControlProduct.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Token { get; set; }
        [Required(ErrorMessage = "Insira uma senha.")]
        [MinLength(6, ErrorMessage ="A senha deve ser maior que 6 caracteres.")]
        public string Senha { get; set; }
    }
}

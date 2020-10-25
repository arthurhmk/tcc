using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Collections.Generic;

namespace ControlProduct.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Esse campo é obrigatório.")]
        [DisplayName("Nome da Categoria")]
        public string Nome { get; set; }

        public int? SuperCategoriaId { get; set; }

        public Categoria SuperCategoria { get; set; }
        public ICollection<Categoria> SubCategorias { get; set; }
    }
}

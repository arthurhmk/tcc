namespace ControlProduct.Models.ViewModel
{
    public class CategoriaViewModel
    {
        public CategoriaViewModel(Categoria cat)
        {
            Id = cat.Id;
            Nome = cat.Nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }
}

namespace ControlProduct.Models.ViewModel
{
    public class ClienteViewModel
    {
        public ClienteViewModel(Cliente cliente)
        {
            Id = cliente.Id;
            Nome = cliente.Nome;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
    }
}

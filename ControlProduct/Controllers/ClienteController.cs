using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlProduct.Models;
using ControlProduct.Repository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlProduct.Controllers
{
    public class ClienteController : Controller
    {
        BaseRepository<Cliente> _repoCliente;
        public ClienteController(BaseRepository<Cliente> repoCliente)
        {
            _repoCliente = repoCliente;
        }
        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            var clientes = await _repoCliente.FromQuery("select * from cliente;");
            return View(clientes);
        }

        public IActionResult NovoCliente()
        {
            return View();
        }
    }
}

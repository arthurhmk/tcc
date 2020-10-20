using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlProduct.Controllers.Common;
using ControlProduct.Models;
using ControlProduct.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlProduct.Controllers
{
    [Route("cliente")]
    public class ClienteController : BaseController
    {
        BaseRepository<Cliente> _repoCliente;
        public ClienteController(BaseServices serv, 
            BaseRepository<Cliente> repoCliente)
            :base(serv)
        {
            _repoCliente = repoCliente;
        }

        [Route("")]
        public IActionResult Index()
        {
            var clientes =  _repoCliente.Entity.AsNoTracking().OrderBy(p=> p.Id).ToList();
            return View(clientes);
        }

        [Route("novo-cliente")]
        public IActionResult CadastroCliente()
        {
            return View(new Cliente());
        }

        [HttpPost]
        [Route("novo-cliente")]
        public IActionResult CadastroCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _repoCliente.Entity.Add(cliente);
                _repoCliente.Context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                throw new Exception("Cliente inválido");
            }
        }
    }
}

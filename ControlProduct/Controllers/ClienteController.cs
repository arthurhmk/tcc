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
        public async Task<IActionResult> Index()
        {
            var clientes =  await _repoCliente.Entity.AsNoTracking().OrderBy(p=> p.Id).ToListAsync();
            return View(clientes);
        }

        [Route("novo-cliente")]
        public async Task<IActionResult> CadastroCliente(int? idCliente)
        {
            if(idCliente != null)
            {
                var cliente = await _repoCliente.Entity.FindAsync(idCliente);
                if (cliente != null)
                    return View(cliente);
            }
            return View(new Cliente());
        }

        [HttpPost]
        [Route("novo-cliente")]
        public async Task<IActionResult> CadastroCliente(Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                if (cliente.Id != 0)
                    await _repoCliente.Update(cliente);
                else
                    await _repoCliente.Insert(cliente);
                return RedirectToAction(nameof(Index));
            }

            throw new Exception("Cliente inválido");
        }

        [Route("remover-cliente")]
        public async Task<IActionResult> RemoverCliente(int? idCliente)
        {
            if (idCliente != null)
            {
                var cliente = await _repoCliente.Entity.FindAsync(idCliente);
                if (cliente != null)
                {
                    await _repoCliente.Delete(cliente);
                    return RedirectToAction(nameof(Index));
                }
            }
            throw new Exception("Cliente inválido");
        }
    }
}

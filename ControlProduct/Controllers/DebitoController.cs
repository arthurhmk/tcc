using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlProduct.Controllers.Common;
using ControlProduct.Models;
using ControlProduct.Models.ViewModel;
using ControlProduct.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ControlProduct.Controllers
{
    [Route("debito")]
    public class DebitoController : BaseController
    {
        BaseRepository<Debito> _repoDebito;
        public DebitoController(BaseServices serv, 
            BaseRepository<Debito> repoDebito)
            :base(serv)
        {
            _repoDebito = repoDebito;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var debitos =  await _repoDebito.Entity.AsNoTracking().OrderBy(p=> p.Id).ToListAsync();
            return View(debitos);
        }

        [Route("novo-debito")]
        public async Task<IActionResult> CadastroDebito(int? idDebito)
        {
            if (idDebito != null)
            {
                var debitos = await _repoDebito.Entity.FindAsync(idDebito);
                if (debitos != null)
                    return View(debitos);
            }
            return View(new Debito());
        }

        [HttpPost]
        [Route("novo-debito")]
        public async Task<IActionResult> CadastroDebito(Debito debito)
        {
            if (ModelState.IsValid)
            {
                if (debito.Id != 0)
                    await _repoDebito.Update(debito);
                else
                    await _repoDebito.Insert(debito);
                return RedirectToAction(nameof(Index));
            }

            throw new Exception("Débito inválido");
        }
    }
}

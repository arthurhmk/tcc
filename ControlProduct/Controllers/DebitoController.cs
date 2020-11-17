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
    }
}

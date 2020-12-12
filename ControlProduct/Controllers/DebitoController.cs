using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ControlProduct.Controllers.Common;
using ControlProduct.Models;
using ControlProduct.Models.Enum;
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
        BaseRepository<Pedido> _repoPedido;
        BaseRepository<PedidoExtra> _repoPedidoExtra;

        public DebitoController(BaseServices serv, 
            BaseRepository<Debito> repoDebito,
            BaseRepository<Pedido> repoPedido,
            BaseRepository<PedidoExtra> repoPedidoExtra)
            :base(serv)
        {
            _repoDebito = repoDebito;
            _repoPedido = repoPedido;
            _repoPedidoExtra = repoPedidoExtra;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var debitos =  (await _repoDebito.Entity.AsNoTracking().OrderBy(p=> p.Data).ToListAsync())
                .Select(p=> new DebitoViewModel(p)).ToList();
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
                List<double> listaPedidoExtra = new List<double>();

                var pedidos = _repoPedido.Entity.AsNoTracking().Where(p => p.Estado == EstadoPedido.PAGO 
                                                                        && p.DataEntrega <= debito.Data
                                                                        && p.DataEntrega.Month == debito.Data.Month)
                                                                        .ToListAsync()
                                                                        .Result;

                listaPedidoExtra.AddRange(pedidos.Select(u => u.Valor));

                pedidos.ForEach(pe => {
                    listaPedidoExtra.AddRange(
                        _repoPedidoExtra.Entity.AsNoTracking()
                            .Where(p => p.Id == pe.Id)
                            .Select(u => u.Valor)
                    );
                });

                debito.Entrada = (decimal)listaPedidoExtra.Sum();

                if (debito.Id != 0)
                    await _repoDebito.Update(debito);
                else
                    await _repoDebito.Insert(debito);
                return Json(new { route = "/debito" });
            }

            throw new Exception("Débito inválido");
        }

        [Route("remover-debito")]
        public async Task<IActionResult> RemoverDebito(int? idDebito)
        {
            if (idDebito != null)
            {
                var debito = await _repoDebito.Entity.FindAsync(idDebito);
                if (debito != null)
                {
                    await _repoDebito.Delete(debito);
                    return Json(new { route = "/debito" });
                }
            }

            throw new Exception("Debito inválida");
        }
    }
}

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
    [Route("")]
    public class PedidoController : BaseController
    {
        BaseRepository<Pedido> _repoPedido;
        BaseRepository<Categoria> _repoCategoria;
        BaseRepository<Produto> _repoProduto;

        public PedidoController(BaseServices serv, 
            BaseRepository<Pedido> repoPedido,
            BaseRepository<Categoria> repoCategoria,
            BaseRepository<Produto> repoProduto)
            :base(serv)
        {
            _repoPedido = repoPedido;
            _repoCategoria = repoCategoria;
            _repoProduto = repoProduto;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var pedidos =  await _repoPedido.Entity.AsNoTracking()
                .Include(p=> p.Cliente)
                .Include(p=> p.Pagamentos)
                .Include(p=> p.PedidoProdutos)
                .OrderBy(p=> p.Id).ToListAsync();
            var model = pedidos.Select(p => new PedidoViewModel(p)).ToList();

            return View(model);
        }

        [Route("novo-pedido")]
        public async Task<IActionResult> CadastroPedido(int? idPedido)
        {
            var produtosNoCat = await _repoProduto.Entity.AsNoTracking().Where(p=>p.CategoriaId==0).ToListAsync();
            var categorias = await _repoCategoria.Entity.AsNoTracking().Include(p=>p.Produtos).ToListAsync();
            ViewBag.produtosNoCat = produtosNoCat;
            ViewBag.categorias = categorias;

            if (idPedido != null)
            {
                var pedidos = await _repoPedido.Entity.Where(p => p.Id == idPedido).ToListAsync();
                if (pedidos.Any())
                    return View(pedidos.First());
            }

            return View(new Pedido());
        }

        [HttpPost]
        [Route("novo-pedido")]
        public async Task<IActionResult> CadastroPedido(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                if (pedido.Id != 0)
                    await _repoPedido.Update(pedido);
                else
                {
                    pedido.DataPedido = DateTime.Now;
                    pedido.Estado = Models.Enum.EstadoPedido.PENDENTE;
                    await _repoPedido.Insert(pedido);
                }
                return RedirectToAction(nameof(Index));
            }

            throw new Exception("Pedido inválido");
        }

        [Route("remover-pedido")]
        public async Task<IActionResult> RemoverPedido(int? idPedido)
        {
            if (idPedido != null)
            {
                var pedido = await _repoPedido.Entity.FindAsync(idPedido);
                if (pedido != null)
                {
                    await _repoPedido.Delete(pedido);
                    return RedirectToAction(nameof(Index));
                }
            }

            throw new Exception("Pedido inválido");
        }
    }
}

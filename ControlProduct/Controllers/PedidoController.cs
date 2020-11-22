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
        BaseRepository<Cliente> _repoCliente;
        BaseRepository<PedidoProduto> _repoPedidoProduto;

        public PedidoController(BaseServices serv, 
            BaseRepository<Pedido> repoPedido,
            BaseRepository<Categoria> repoCategoria,
            BaseRepository<Produto> repoProduto,
            BaseRepository<Cliente> repoCliente,
            BaseRepository<PedidoProduto> repoPedidoProduto)
            :base(serv)
        {
            _repoPedido = repoPedido;
            _repoCategoria = repoCategoria;
            _repoProduto = repoProduto;
            _repoCliente = repoCliente;
            _repoPedidoProduto = repoPedidoProduto;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var pedidos =  await _repoPedido.Entity.AsNoTracking()
                .Include(p=> p.Cliente)
                .Include(p=> p.Pagamentos)
                .Include(p=> p.PedidoProdutos).ThenInclude(p=> p.Produto)
                .OrderBy(p=> p.Id).ToListAsync();
            var model = pedidos.Select(p => new PedidoViewModel(p)).OrderByDescending(p=>p.Id).ToList();

            return View(model);
        }

        [Route("novo-pedido")]
        public async Task<IActionResult> CadastroPedido(int? idPedido)
        {
            var produtosNoCat = await _repoProduto.Entity.AsNoTracking().Where(p=>p.CategoriaId==0).ToListAsync();
            var categorias = await _repoCategoria.Entity.AsNoTracking().Include(p=>p.Produtos).ToListAsync();
            var clientes = await _repoCliente.Entity.AsNoTracking().ToListAsync();
            ViewBag.produtosNoCat = produtosNoCat;
            ViewBag.categorias = categorias;
            ViewBag.clientes = clientes;

            if (idPedido != null)
            {
                var pedidos = await _repoPedido.Entity.AsNoTracking().Where(p => p.Id == idPedido).Include(p=>p.PedidoProdutos).Include(p=>p.Pagamentos).ToListAsync();
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
                pedido.PedidoProdutos.ForEach(p => p.Pedido = pedido);
                if (pedido.Pagamentos != null)
                {
                    pedido.Pagamentos.ForEach(p => p.Pedido = pedido);
                }

                if (pedido.Id != 0)
                {
                    var oldPedido = await _repoPedido.Entity.AsNoTracking().Include(p=>p.PedidoProdutos).ThenInclude(p=>p.Produto).Include(p=>p.Pagamentos).Where(p=>p.Id == pedido.Id).ToListAsync();
                    if (oldPedido.Any())
                    {
                        if(pedido.Pagamentos != null && oldPedido.First().Pagamentos.Any())
                        {
                            pedido.Pagamentos.First().Id = oldPedido.First().Pagamentos.First().Id;
                        }

                        //Atrubuir o Id de produtos já cadastrados no pedido
                        PedidoProduto temp;
                        pedido.PedidoProdutos.ForEach(p => p.Id = (temp = oldPedido.First().PedidoProdutos.FirstOrDefault(q => q.IdProduto == p.IdProduto)) == null? 0 : temp.Id);

                        pedido.DataPedido = oldPedido.First().DataPedido;
                        await _repoPedido.Update(pedido);

                        var produtosRemover = oldPedido.First().PedidoProdutos.Where(p => !pedido.PedidoProdutos.Select(q => q.IdProduto).Contains(p.IdProduto));
                        foreach(var pr in produtosRemover)
                        {
                            pr.Produto = null;
                            pr.Pedido = null;
                            await _repoPedidoProduto.Delete(pr);
                        }
                    }
                }
                else
                {
                    pedido.DataPedido = DateTime.Now;
                    await _repoPedido.Insert(pedido);
                }
                return Json(new { route = "/" });
            }

            throw new Exception("Pedido inválido");
        }

        [Route("remover-pedido")]
        public async Task<IActionResult> RemoverPedido(int? idPedido)
        {
            if (idPedido != null)
            {
                var pedido = await _repoPedido.Entity.Include(p=>p.PedidoProdutos).Where(p=>p.Id == idPedido).ToListAsync();
                if (pedido != null)
                {
                    await _repoPedido.Delete(pedido.First());
                    return RedirectToAction(nameof(Index));
                }
            }

            throw new Exception("Pedido inválido");
        }
    }
}

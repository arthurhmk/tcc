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
        BaseRepository<PedidoExtra> _repoExtra;

        public PedidoController(BaseServices serv, 
            BaseRepository<Pedido> repoPedido,
            BaseRepository<Categoria> repoCategoria,
            BaseRepository<Produto> repoProduto,
            BaseRepository<Cliente> repoCliente,
            BaseRepository<PedidoProduto> repoPedidoProduto,
            BaseRepository<PedidoExtra> repoExtra)
            :base(serv)
        {
            _repoPedido = repoPedido;
            _repoCategoria = repoCategoria;
            _repoProduto = repoProduto;
            _repoCliente = repoCliente;
            _repoPedidoProduto = repoPedidoProduto;
            _repoExtra = repoExtra;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var pedidos =  await _repoPedido.Entity.AsNoTracking()
                .Include(p=> p.Cliente)
                .Include(p=> p.Pagamentos)
                .Include(p=> p.PedidoProdutos).ThenInclude(p=> p.Produto)
                .Include(p=> p.Extras)
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

            if (!clientes.Any())
                return RedirectToAction(nameof(WarnController.Cliente), "error");

            if (!categorias.SelectMany(p=>p.Produtos).Any())
                return RedirectToAction(nameof(WarnController.Produto), "error");

            if (idPedido != null)
            {
                var pedidos = await _repoPedido.Entity.AsNoTracking()
                    .Include(p => p.PedidoProdutos)
                    .Include(p => p.Pagamentos)
                    .Include(p=> p.Extras)
                    .Where(p => p.Id == idPedido).ToListAsync();
                if (pedidos.Any())
                    return View(pedidos.First());
            }

            return View(new Pedido());
        }

        [HttpPost]
        [Route("novo-pedido")]
        public async Task<IActionResult> CadastroPedido(Pedido pedido)
        {
            if (ModelState.IsValid && (pedido.PedidoProdutos != null || pedido.Extras != null))
            {
                if (pedido.PedidoProdutos != null)
                    pedido.PedidoProdutos.ForEach(p => p.Pedido = pedido);

                if (pedido.Extras != null)
                    pedido.Extras.ForEach(p => p.Pedido = pedido);

                if (pedido.Pagamentos != null)
                    pedido.Pagamentos.ForEach(p => p.Pedido = pedido);

                if (pedido.Id != 0)
                {
                    var oldPedido = await _repoPedido.Entity.AsNoTracking()
                        .Include(p=>p.PedidoProdutos).ThenInclude(p=>p.Produto)
                        .Include(p=>p.Pagamentos)
                        .Include(p=>p.Extras)
                        .Where(p=>p.Id == pedido.Id).ToListAsync();

                    if (oldPedido.Any())
                    {
                        if(pedido.Pagamentos != null && oldPedido.First().Pagamentos.Any())
                        {
                            pedido.Pagamentos.First().Id = oldPedido.First().Pagamentos.First().Id;
                        }

                        if (pedido.PedidoProdutos != null)
                        {
                            //Atribuir o Id de produtos já cadastrados no pedido
                            PedidoProduto temp;
                            pedido.PedidoProdutos.ForEach(p => p.Id = (temp = oldPedido.First().PedidoProdutos.FirstOrDefault(q => q.IdProduto == p.IdProduto)) == null ? 0 : temp.Id);
                        }
                        if (pedido.Extras != null)
                        {
                            PedidoExtra temp;
                            pedido.Extras.ForEach(p => p.Id = (temp = oldPedido.First().Extras.FirstOrDefault(q => q.Id == p.Id)) == null ? 0 : temp.Id);
                        }


                        pedido.DataPedido = oldPedido.First().DataPedido;
                         await _repoPedido.Update(pedido);


                        if (pedido.PedidoProdutos != null)
                        {
                            var produtosRemover = oldPedido.First().PedidoProdutos.Where(p => !pedido.PedidoProdutos.Select(q => q.IdProduto).Contains(p.IdProduto));
                            foreach (var pr in produtosRemover)
                            {
                                pr.Produto = null;
                                pr.Pedido = null;
                                await _repoPedidoProduto.Delete(pr);
                            }
                        }
                        if (pedido.Extras != null)
                        {
                            var extrasRemover = oldPedido.First().Extras.Where(p => !pedido.Extras.Select(q => q.Id).Contains(p.Id));
                            foreach (var pr in extrasRemover)
                            {
                                pr.Pedido = null;
                                await _repoExtra.Delete(pr);
                            }
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
                var pedido = await _repoPedido.Entity.Include(p=>p.PedidoProdutos).Include(p=>p.Extras).Where(p=>p.Id == idPedido).ToListAsync();
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

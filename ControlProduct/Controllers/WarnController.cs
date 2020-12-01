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
    [Route("error")]
    public class WarnController : BaseController
    {
        BaseRepository<Pedido> _repoPedido;
        BaseRepository<Categoria> _repoCategoria;
        BaseRepository<Produto> _repoProduto;
        BaseRepository<Cliente> _repoCliente;
        BaseRepository<PedidoProduto> _repoPedidoProduto;

        public WarnController(BaseServices serv,
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

        [Route("cliente")]
        public async Task<IActionResult> Cliente()
        {
            ViewBag.Controller = "Cliente";
            if (!(await _repoCliente.Entity.AsNoTracking().ToListAsync()).Any())
                return View("Index", "Não foram encontrados clientes registrados. Cadastre um antes de continuar.");
            else
                return RedirectToAction(nameof(PedidoController.Index) ,"Pedido");
        }

        [Route("produto")]
        public async Task<IActionResult> Produto()
        {
            ViewBag.Controller = "Produto";
            if (!(await _repoProduto.Entity.AsNoTracking().ToListAsync()).Any())
                return View("Index", "Não foram encontrados produtos cadastrados. Crie um produto antes de continuar.");
            else
                return RedirectToAction(nameof(ProdutoController.Index), "Produto");
        }

        [Route("categoria")]
        public async Task<IActionResult> Categoria()
        {
            ViewBag.Controller = "Categoria";
            if (!(await _repoProduto.Entity.AsNoTracking().ToListAsync()).Any())
                return View("Index", "Não foram encontrados categorias para o produto. Cadastre uma categoria antes de continuar.");
            else
                return RedirectToAction(nameof(CategoriaController.Index), "Categoria");
        }
    }
}

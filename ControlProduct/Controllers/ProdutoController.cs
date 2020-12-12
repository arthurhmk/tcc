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
    [Route("produto")]
    public class ProdutoController : BaseController
    {
        BaseRepository<Produto> _repoProduto;
        BaseRepository<Categoria> _repoCategoria;

        public ProdutoController(BaseServices serv, 
            BaseRepository<Categoria> repoCategoria,
            BaseRepository<Produto> repoProduto)
            :base(serv)
        {
            _repoProduto = repoProduto;
            _repoCategoria = repoCategoria;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var produtos =  await _repoProduto.Entity.AsNoTracking().Include(p=>p.Categoria).OrderBy(p=> p.Id).ToListAsync();
            return View(produtos);
        }

        [Route("novo-produto")]
        public async Task<IActionResult> CadastroProduto(int? idProduto)
        {
            var categorias = await _repoCategoria.Entity.AsNoTracking().ToListAsync();
            ViewBag.categorias = categorias;

            if (!categorias.Any())
                return RedirectToAction(nameof(WarnController.Categoria), "error");

            if (idProduto != null)
            {
                var produtos = await _repoProduto.Entity.AsNoTracking().Include(p => p.Categoria).Where(p => p.Id == idProduto).ToListAsync();
                if (produtos.Any())
                    return View(produtos.First());
            }
            return View(new Produto());
        }

        [HttpPost]
        [Route("novo-produto")]
        public async Task<IActionResult> CadastroProduto(Produto produto)
        {
            if (ModelState.IsValid)
            {
                if (produto.Id != 0)
                    await _repoProduto.Update(produto);
                else
                {
                    var oldProduto = await _repoProduto.Entity.AsNoTracking().Where(p => p.Nome.ToUpper() == produto.Nome.ToUpper()).ToListAsync();
                    if (oldProduto.Any())
                        throw new Exception("Produto já existente");    
                    await _repoProduto.Insert(produto);
                }

                return Json(new { route = "/produto" });
            }

            throw new Exception("Produto inválido");
        }

        [Route("remover-produto")]
        public async Task<IActionResult> RemoverProduto(int? idProduto)
        {
            if (idProduto != null)
            {
                var produto = await _repoProduto.Entity.FindAsync(idProduto);
                if (produto != null)
                {
                    await _repoProduto.Delete(produto);
                    return Json(new { route = "/produto" });
                }
            }

            throw new Exception("Produto inválido");
        }
    }
}

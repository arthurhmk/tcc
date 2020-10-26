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
    [Route("categoria")]
    public class CategoriaController : BaseController
    {
        BaseRepository<Categoria> _repoCategoria;
        public CategoriaController(BaseServices serv, 
            BaseRepository<Categoria> repoCategoria)
            :base(serv)
        {
            _repoCategoria = repoCategoria;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            var categorias =  await _repoCategoria.Entity.AsNoTracking().Include(p=>p.SuperCategoria).OrderBy(p=> p.Id).ToListAsync();
            return View(categorias);
        }

        [Route("nova-categoria")]
        public async Task<IActionResult> CadastroCategoria(int? idCategoria)
        {
            if(idCategoria != null)
            {
                var categorias = await _repoCategoria.Entity.FindAsync(idCategoria);
                if (categorias != null)
                    return View(categorias);
            }
            return View(new Categoria());
        }

        [HttpPost]
        [Route("nova-categoria")]
        public async Task<IActionResult> CadastroCategoria(Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                if (categoria.Id != 0)
                    await _repoCategoria.Update(categoria);
                else
                    await _repoCategoria.Insert(categoria);
                return RedirectToAction(nameof(Index));
            }

            throw new Exception("Categoria inválida");
        }

        [Route("remover-categoria")]
        public async Task<IActionResult> RemoverCategoria(int? idCategoria)
        {
            if (idCategoria != null)
            {
                var categoria = await _repoCategoria.Entity.FindAsync(idCategoria);
                if (categoria != null)
                {
                    await _repoCategoria.Delete(categoria);
                    return RedirectToAction(nameof(Index));
                }
            }

            throw new Exception("Categoria inválida");
        }

        [HttpPost]
        [Route("buscar-super-categorias")]
        public async Task<IActionResult> BuscarSuperCategorias(int idCategoria)
        {
            if (ModelState.IsValid) {
                bool hasSubCategorias = (await _repoCategoria.Entity.AsNoTracking().Where(p => p.SuperCategoriaId == idCategoria).ToListAsync()).Any();

                if (!hasSubCategorias)
                {
                    var superCategorias = (await _repoCategoria.Entity.AsNoTracking()
                        .Where(p => p.SuperCategoriaId == null && idCategoria != p.Id)
                        .OrderBy(p => p.Id)
                        //.Skip(0).Take(20)
                        .ToListAsync())
                    .Select(p => new CategoriaViewModel(p));

                    return Json(superCategorias);
                }
                else
                {
                    return Json(new List<CategoriaViewModel>());
                }
            }
            throw new Exception("Id inválido");
        }

        [HttpPost]
        [Route("buscar-categorias")]
        public async Task<IActionResult> BuscarCategorias(int? idCategoria)
        {
            var categorias = (await _repoCategoria.Entity.AsNoTracking()
                    .Where(p=> idCategoria == null || idCategoria != p.Id)
                    .OrderBy(p => p.Id)
                    //.Skip(0).Take(20)
                    .ToListAsync())
                .Select(p => new CategoriaViewModel(p));

            return Json(categorias);
        }
    }
}

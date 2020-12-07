using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ControlProduct.Models;
using ControlProduct.Models.ViewModel;
using ControlProduct.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace ControlProduct.Controllers.Common
{
    public class BaseController : Controller
    {
        protected readonly ILogger<BaseServices> _logger;
        private readonly BaseRepository<User> _userRepo;

        public BaseController(BaseServices serv)
        {
            _logger = serv._logger;
            _userRepo = serv._userRepo;
        }

        [Route("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.UserAutenticated = false;
            if(context.Controller.GetType().Name != nameof(UserController))
            {
                if (HttpContext.Request.Cookies["user-token"] != null)
                {
                    var token = HttpContext.Request.Cookies["user-token"];

                    var loggedUser = _userRepo.Entity.AsNoTracking().Where(p => p.Token == token).ToList();
                    if (loggedUser.Count > 0)
                    {
                        ViewBag.UserAutenticated = true;
                    }
                    else
                    {
                        context.Result = RedirectToAction(nameof(UserController.Index), "User");
                    }
                }
                else
                {
                    context.Result = RedirectToAction(nameof(UserController.Index), "User");
                }
            }

            base.OnActionExecuting(context);
        }
    }
}

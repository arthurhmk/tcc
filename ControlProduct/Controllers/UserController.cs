using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
    [Route("user")]
    public class UserController : BaseController
    {
        BaseRepository<User> _repoUser;

        public UserController(BaseServices serv,
            BaseRepository<User> repoUser)
            :base(serv)
        {
            _repoUser = repoUser;
        }

        [Route("")]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [Route("login")]
        public async Task<IActionResult> Login(User user)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(user.Senha))
            {
                using SHA256 sha256 = SHA256.Create();
                StringBuilder hash = new StringBuilder();
                foreach (byte b in sha256.ComputeHash(Encoding.UTF8.GetBytes(user.Senha)))
                    hash.AppendFormat("{0:X2}", b);
                var logginIn = await _repoUser.Entity.AsNoTracking().Where(p => p.Senha == hash.ToString()).ToListAsync();
                if (logginIn.Any())
                {
                    byte[] buffer = new byte[32];
                    new RNGCryptoServiceProvider().GetBytes(buffer);

                    StringBuilder token = new StringBuilder();
                    foreach (byte b in buffer)
                        token.AppendFormat("{0:X2}", b);
                    logginIn.First().Token = token.ToString();
                    await _repoUser.Update(logginIn.First());

                    return Json(new { token = token.ToString(), route = "/" });
                }
            }
            throw new Exception("Senha inválida");
        }

        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            var token = HttpContext.Request.Cookies["user-token"];
            var logginOut = await _repoUser.Entity.AsNoTracking().Where(p => p.Token == token).ToListAsync();
            foreach(var logout in logginOut)
            {
                logout.Token = null;
                await _repoUser.Update(logout);
            }
            return RedirectToAction(nameof(UserController.Index), "User");
        }


        [Route("password")]
        public async Task<IActionResult> TrocarSenha(User user)
        {
            return null;
        }
    }
}

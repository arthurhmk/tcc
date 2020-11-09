using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ControlProduct.Models.ViewModel;
using ControlProduct.Controllers.Common;

namespace ControlProduct.Controllers
{
    [Route("OldHome")]
    public class HomeController : BaseController
    {

        public HomeController(BaseServices serv)
            :base(serv)
        {
        }

        public IActionResult Index()
        {
            return View();
        }        
    }
}

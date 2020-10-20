using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlProduct.Controllers.Common
{
    public class BaseServices
    {
        public readonly ILogger<BaseServices> _logger;

        public BaseServices(ILogger<BaseServices> logger)
        {
            _logger = logger;
        }
    }
}

using ControlProduct.Models;
using ControlProduct.Repository;
using Microsoft.Extensions.Logging;

namespace ControlProduct.Controllers.Common
{
    public class BaseServices
    {
        public readonly ILogger<BaseServices> _logger;
        public readonly BaseRepository<User> _userRepo;

        public BaseServices(ILogger<BaseServices> logger, BaseRepository<User> userRepo)
        {
            _logger = logger;
            _userRepo = userRepo;
        }
    }
}

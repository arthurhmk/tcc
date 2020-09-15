using ControlProduct.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ControlProduct.Repository
{
    public class BaseContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }

        public BaseContext(DbContextOptions<BaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new Config.Cliente());
        }
    }
}

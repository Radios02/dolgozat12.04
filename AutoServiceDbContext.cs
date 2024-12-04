using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dolgozat12._04
{
    public class AutoServiceDbContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<ServiceLog> ServiceLogs { get; set; }
        public DbSet<ServiceDetail> ServiceDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer($"Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=dolgozat12._04;Integrated Security=true");
        }
    }

}

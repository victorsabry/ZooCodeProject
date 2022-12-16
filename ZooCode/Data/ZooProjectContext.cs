using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZooCode.Models;

namespace ZooCode.Data
{
    public class ZooProjectContext : DbContext
    {
        public ZooProjectContext (DbContextOptions<ZooProjectContext> options)
            : base(options)
        {
        }

        public DbSet<ZooCode.Models.Animal> Animal { get; set; } = default!;

        public DbSet<ZooCode.Models.Zoo> Zoo { get; set; }

        public DbSet<ZooCode.Models.ZooAnimal> ZooAnimal { get; set; }
    }
}

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Starting values for 3 different Zoos
            modelBuilder.Entity<Zoo>().HasData(new Zoo { ZooID = 1, Zoo_name = "Zealand Zoo", Zoo_address = "Maglegaardsvej 2" });
            modelBuilder.Entity<Zoo>().HasData(new Zoo { ZooID = 2, Zoo_name = "Aalborg Zoo", Zoo_address = "Aalborg torv" });
            modelBuilder.Entity<Zoo>().HasData(new Zoo { ZooID = 3, Zoo_name = "Køge Fuglebur", Zoo_address = "Køge Park" });

            //Starting values for 3 different Animals
            modelBuilder.Entity<Animal>().HasData(new Animal { AnimalID = 1, Animal_name = "Tiger" });
            modelBuilder.Entity<Animal>().HasData(new Animal { AnimalID = 2, Animal_name = "Panda" });
            modelBuilder.Entity<Animal>().HasData(new Animal { AnimalID = 3, Animal_name = "Eagle" });

            //Starting values for 3 different relations between Zoo and Animal
            modelBuilder.Entity<ZooAnimal>().HasData(new ZooAnimal { ZooAnimalID = 1, ZooID = 1, AnimalID = 1 });
            modelBuilder.Entity<ZooAnimal>().HasData(new ZooAnimal { ZooAnimalID = 2, ZooID = 2, AnimalID = 2 });
            modelBuilder.Entity<ZooAnimal>().HasData(new ZooAnimal { ZooAnimalID = 3, ZooID = 3, AnimalID = 3 });
        }
        public DbSet<ZooCode.Models.Animal> Animal { get; set; } = default!;

        public DbSet<ZooCode.Models.Zoo> Zoo { get; set; }

        public DbSet<ZooCode.Models.ZooAnimal> ZooAnimal { get; set; }
    }
}

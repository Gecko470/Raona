using Jones_Bank.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Jones_Bank
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
             : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Cuenta> Cuentas { get; set; } = null!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=DefaultConnection");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cuenta>().HasData(new Cuenta { Id = 1, NumeroCuenta = "ES012345678901234567", Cliente = "Pedro García", Saldo = 2500.50M });
            modelBuilder.Entity<Cuenta>().HasData(new Cuenta { Id = 2, NumeroCuenta = "FR012345678901234567", Cliente = "Francisco López", Saldo = 3740.20M });
            modelBuilder.Entity<Cuenta>().HasData(new Cuenta { Id = 3, NumeroCuenta = "BE012345678901234567", Cliente = "María Pérez", Saldo = 1765 });
            modelBuilder.Entity<Cuenta>().HasData(new Cuenta { Id = 4, NumeroCuenta = "IT012345678901234567", Cliente = "Susana Sanz", Saldo = 5428.75M });

            modelBuilder.Entity<User>().HasData(new User { Id = 1, Nombre = "María García", Email = "mariaGr@gmail.com", Pass = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" });
            modelBuilder.Entity<User>().HasData(new User { Id = 2, Nombre = "Susana Sanz", Email = "susAz@gmail.com", Pass = "03ac674216f3e15c761ee1a5e255f067953623c8b388b4459e13f978d7c846f4" });
        }

    }
}

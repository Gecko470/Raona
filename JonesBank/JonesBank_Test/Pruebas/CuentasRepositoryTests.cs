using Jones_Bank;
using Jones_Bank.Controllers;
using Jones_Bank.DTOs;
using Jones_Bank.Models;
using Jones_Bank.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JonesBank_Test.Pruebas
{
    [TestClass]
    public class CuentasRepositoryTests
    {
        private DbContextOptions<AppDbContext> _options;

        [TestInitialize]
        public void Initialize()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDatabase_{Guid.NewGuid()}")
                .Options;

            using (var context = new AppDbContext(_options))
            {
                context.Cuentas.AddRange(
                    new Cuenta() { Id = 1, NumeroCuenta = "ES012345678901234567", Cliente = "Pedro García", Saldo = 2500 },
                    new Cuenta() { Id = 2, NumeroCuenta = "FR012345678901234567", Cliente = "Francisco López", Saldo = 2500 },
                    new Cuenta() { Id = 3, NumeroCuenta = "BE012345678901234567", Cliente = "María Pérez", Saldo = 2500 },
                    new Cuenta() { Id = 4, NumeroCuenta = "IT012345678901234567", Cliente = "Susana Sanz", Saldo = 2500 }
                );
                context.SaveChangesAsync().Wait();
            }
        }

        [TestMethod]
        public async Task GetAllDevuelveListaCuentas()
        {

            using (var context = new AppDbContext(_options))
            {

                var repository = new CuentasRepository(context);

                var resultado = await repository.GetAll();


                Assert.AreEqual(4, resultado.Count);
            }
        }

        [TestMethod]
        public async Task GetByNumCuentaDevuelveCuenta()
        {

            using (var context = new AppDbContext(_options))
            {

                var repository = new CuentasRepository(context);

                var resultado = await repository.GetByNumCuenta("ES012345678901234567");


                Assert.IsInstanceOfType(resultado, typeof(Cuenta));
            }
        }

        [TestMethod]
        public async Task GetByNumCuentaDevuelveNull()
        {

            using (var context = new AppDbContext(_options))
            {

                var repository = new CuentasRepository(context);

                var resultado = await repository.GetByNumCuenta("ES01234567890123456");


                Assert.AreEqual((Cuenta?)null, resultado);
            }
        }

        [TestMethod]
        public async Task ModificarSaldoAcutalizaSaldo()
        {

            using (var context = new AppDbContext(_options))
            {

                var repository = new CuentasRepository(context);

                ModificarSaldoDTO modifSaldo = new ModificarSaldoDTO() { NumCuenta = "ES012345678901234567", Importe = 200 };

                await repository.ModificarSaldo(modifSaldo);

                var cuentaActualizada = await context.Cuentas.FirstOrDefaultAsync(x => x.NumeroCuenta == modifSaldo.NumCuenta);

                Assert.AreEqual(2700, cuentaActualizada.Saldo);
            }
        }

        [TestMethod]
        public async Task GetCapitalTotalDevueveDiezMil()
        {

            using (var context = new AppDbContext(_options))
            {

                var repository = new CuentasRepository(context);

                var resultado = await repository.GetCapitalTotal();

                Assert.AreEqual(10000, resultado);
            }
        }
    }
}
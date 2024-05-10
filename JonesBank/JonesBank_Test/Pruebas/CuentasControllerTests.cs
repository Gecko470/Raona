using Jones_Bank;
using Jones_Bank.Controllers;
using Jones_Bank.DTOs;
using Jones_Bank.Models;
using Jones_Bank.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;
using Moq;

namespace JonesBank_Test.Pruebas
{
    [TestClass]
    public class CuentasControllerTests
    {
        [TestMethod]
        public async Task GetAllDevuelveListaCuentas()
        {
            var listaCuentasEsperada = new List<Cuenta>
                {
                new Cuenta(){ Id = 1, NumeroCuenta = "ES012345678901234567", Cliente = "Pedro García", Saldo = 2500.50M },
                new Cuenta(){ Id = 2, NumeroCuenta = "FR012345678901234567", Cliente = "Francisco López", Saldo = 3740.20M },
                new Cuenta(){ Id = 3, NumeroCuenta = "BE012345678901234567", Cliente = "María Pérez", Saldo = 1765 },
                new Cuenta(){ Id = 4, NumeroCuenta = "IT012345678901234567", Cliente = "Susana Sanz", Saldo = 5428.75M }
                };


            var mockIwebHostEnviroment = new Mock<IWebHostEnvironment>();
            var mockCuentasRepository = new Mock<ICuentasRepository>();
            mockCuentasRepository.Setup(repo => repo.GetAll()).ReturnsAsync(listaCuentasEsperada);

            var controller = new CuentasController(mockCuentasRepository.Object, mockIwebHostEnviroment.Object);

            var resultado = await controller.GetAll();

            Assert.AreEqual(4, resultado.Count);
        }

        [TestMethod]
        public async Task CapitalTotalDevuelveString()
        {
            var mockIwebHostEnviroment = new Mock<IWebHostEnvironment>();
            var mockCuentasRepository = new Mock<ICuentasRepository>();
            decimal capitalEsperado = 13434.45M;

            mockCuentasRepository.Setup(x => x.GetCapitalTotal()).ReturnsAsync(capitalEsperado);

            var controller = new CuentasController(mockCuentasRepository.Object, mockIwebHostEnviroment.Object);

            var resultado = await controller.CapitalTotal();

            var resultadoEsperado = $"El banco JonesBank dispone de un capital total de {capitalEsperado}$";

            Assert.AreEqual(resultadoEsperado, resultado);
        }

        [TestMethod]
        public async Task ModificarSaldoDevuelveNotFound()
        {
            ModificarSaldoDTO modifSaldo = new ModificarSaldoDTO() { NumCuenta = "ES01234567890123456", Importe = 200M };
            var mockIwebHostEnviroment = new Mock<IWebHostEnvironment>();
            var mockCuentasRepository = new Mock<ICuentasRepository>();
            mockCuentasRepository.Setup(repo => repo.GetByNumCuenta(modifSaldo.NumCuenta)).ReturnsAsync((Cuenta?) null);

            var controller = new CuentasController(mockCuentasRepository.Object, mockIwebHostEnviroment.Object);

            var resultado = await controller.ModificarSaldo(modifSaldo);

            Assert.IsInstanceOfType(resultado, typeof(NotFoundResult));
        }

       
        [TestMethod]
        public async Task ModificarSaldoDevuelveBadRequestConImporteNegativo()
        {
            ModificarSaldoDTO modifSaldo = new ModificarSaldoDTO() { NumCuenta = "FR012345678901234567", Importe = -200.00M };
            var mockIwebHostEnviroment = new Mock<IWebHostEnvironment>();
            var mockCuentasRepository = new Mock<ICuentasRepository>();

            mockCuentasRepository.Setup(repo => repo.GetByNumCuenta(modifSaldo.NumCuenta)).ReturnsAsync(new Cuenta());
            //PARA PASAR ESTA PRUEBA ES NECESARIO CONFIGURAR EL RETORNO DE WEBROOTPATH CON LA RUTA REAL DEL EQUIPO
            mockIwebHostEnviroment.Setup(env => env.WebRootPath).Returns("C:\\Users\\juang\\Desktop\\Desarrollo\\NET\\repos\\Proyectos\\Raona\\JonesBank\\JonesBank\\wwwroot");

            var controller = new CuentasController(mockCuentasRepository.Object, mockIwebHostEnviroment.Object);

            var resultado = await controller.ModificarSaldo(modifSaldo);

            Assert.IsInstanceOfType(resultado, typeof(BadRequestResult));

        }

        [TestMethod]
        public async Task ModificarSaldoDevuelveOk()
        {
            ModificarSaldoDTO modifSaldo = new ModificarSaldoDTO() { NumCuenta = "FR012345678901234567", Importe = 200.00M };
            var mockIwebHostEnviroment = new Mock<IWebHostEnvironment>();
            var mockCuentasRepository = new Mock<ICuentasRepository>();

            mockCuentasRepository.Setup(repo => repo.GetByNumCuenta(modifSaldo.NumCuenta)).ReturnsAsync(new Cuenta());
            //PARA PASAR ESTA PRUEBA ES NECESARIO CONFIGURAR EL RETORNO DE WEBROOTPATH CON LA RUTA REAL DEL EQUIPO
            mockIwebHostEnviroment.Setup(env => env.WebRootPath).Returns("C:\\Users\\juang\\Desktop\\Desarrollo\\NET\\repos\\Proyectos\\Raona\\JonesBank\\JonesBank\\wwwroot");

            var controller = new CuentasController(mockCuentasRepository.Object, mockIwebHostEnviroment.Object);

            var resultado = await controller.ModificarSaldo(modifSaldo);

            Assert.IsInstanceOfType(resultado, typeof(OkResult));
        }
    }
}
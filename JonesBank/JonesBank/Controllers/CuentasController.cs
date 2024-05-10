using Jones_Bank.DTOs;
using Jones_Bank.Models;
using Jones_Bank.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.IO;

namespace Jones_Bank.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CuentasController : ControllerBase
    {
        private readonly ICuentasRepository _repo;
        private readonly IWebHostEnvironment _env;

        public CuentasController(ICuentasRepository repo, IWebHostEnvironment env)
        {
            _repo = repo;
            _env = env;
        }

        //LISTADO DE CUENTAS EXISTENTES PARA EL FRONT Y COMO ENDPOINT PARA LA ENTIDAD REGULADORA DEL PAIS
        [HttpGet("getAll")]
        public async Task<List<Cuenta>> GetAll()
        {
            List<Cuenta> lista = new List<Cuenta>();
            try
            {
                lista = await _repo.GetAll();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún error: {ex.Message}");
            }

            return lista;
        }

        //METODO PARA HACER INGRESOS/RETIRADAS DE UNA CUENDA DETERMINADA POR LOS USUARIOS DEL BANCO.
        [HttpPost("modificarSaldo")]
        public async Task<ActionResult> ModificarSaldo(ModificarSaldoDTO modifSaldo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var cuentaBD = await _repo.GetByNumCuenta(modifSaldo.NumCuenta);
                    if (cuentaBD == null)
                    {
                        return NotFound();
                    }
                    if (modifSaldo.Importe < 0)
                    {
                        var fecha = DateTime.Now;
                        var guid = Guid.NewGuid();
                        string archivo = $"Informe_{guid}.txt";
                        string texto = $"\nJonesBank\nFecha: {fecha}. El usuario X ha intentado hacer una retirada de {modifSaldo.Importe}$ en la cuenta con nº {modifSaldo.NumCuenta}";

                        var folder = System.IO.Path.Combine(_env.WebRootPath, "Informes");

                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }

                        string ruta = System.IO.Path.Combine(folder, archivo);

                        using (StreamWriter sw = System.IO.File.CreateText(ruta))
                        {
                            sw.WriteLine(texto);
                        }

                        return new JsonResult(new { pillado = true});

                    }
                    else
                    {
                        await _repo.ModificarSaldo(modifSaldo);
                        return Ok();
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Ha habido algún error: {ex.Message}");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        //METODO PARA LA ENTIDAD REGULADORA PARA OBTENER LA CANTIDAD TOTAL DE CAPITAL DISPONIBLE EN EL BANCO 
        [HttpGet("capitalTotal")]
        public async Task<string> CapitalTotal()
        {
            try
            {
                var capital = await _repo.GetCapitalTotal();
                return $"El banco JonesBank dispone de un capital total de {capital}$";
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún error: {ex.Message}");
            }
        }
    }
}

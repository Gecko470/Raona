using Jones_Bank.DTOs;
using Jones_Bank.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Jones_Bank.Services
{
    public interface ICuentasRepository
    {
        Task<List<Cuenta>> GetAll();
        Task<Cuenta> GetByNumCuenta(string numCuenta);
        Task<decimal> GetCapitalTotal();
        Task ModificarSaldo(ModificarSaldoDTO modifSaldo);
    }


    public class CuentasRepository : ICuentasRepository
    {
        private readonly AppDbContext _dbContext;

        public CuentasRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Cuenta>> GetAll()
        {
            try
            {
                return await _dbContext.Cuentas.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún error: {ex.Message}");
            }
        }


        public async Task<Cuenta> GetByNumCuenta(string numCuenta)
        {
            try
            {
                return await _dbContext.Cuentas.FirstOrDefaultAsync(x => x.NumeroCuenta == numCuenta);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún error: {ex.Message}");
            }

        }


        public async Task ModificarSaldo(ModificarSaldoDTO modifSaldo)
        {
            try
            {
                Cuenta cuentaBD = await _dbContext.Cuentas.FirstOrDefaultAsync(x => x.NumeroCuenta == modifSaldo.NumCuenta);
                cuentaBD.Saldo += modifSaldo.Importe;

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún error: {ex.Message}");
            }
        }

        public async Task<decimal> GetCapitalTotal()
        {
            try
            {
                return await _dbContext.Cuentas.SumAsync(x => x.Saldo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ha habido algún error: {ex.Message}");
            }
        }
    }
}

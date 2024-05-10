using System.ComponentModel.DataAnnotations;

namespace Jones_Bank.Models
{
    public class Cuenta
    {
        public int Id { get; set; }
        [StringLength(20)]
        public string NumeroCuenta { get; set; }
        public string Cliente { get; set; } 
        public decimal Saldo { get; set; }
    }
}

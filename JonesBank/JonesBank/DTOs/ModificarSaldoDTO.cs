using System.ComponentModel.DataAnnotations;

namespace Jones_Bank.DTOs
{
    public class ModificarSaldoDTO
    {
        [Required]
        public string NumCuenta { get; set; }
        [Required]
        public decimal Importe { get; set; }
    }
}

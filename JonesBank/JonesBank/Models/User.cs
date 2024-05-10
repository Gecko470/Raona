using System.ComponentModel.DataAnnotations;

namespace Jones_Bank.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio..")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obligatorio..")]
        public string Pass { get; set; }
    }
}

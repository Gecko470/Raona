using System.ComponentModel.DataAnnotations;

namespace JonesBank.DTOs
{
    public class UserDTO
    {
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Campo obligatorio..")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obligatorio..")]
        public string Pass { get; set; }
    }
}

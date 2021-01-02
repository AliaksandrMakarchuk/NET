using System.ComponentModel.DataAnnotations;

namespace WebRestApi.WebApp.Models
{
    public class Credentials
    {
        [MinLength(2), MaxLength(15), Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [MinLength(2), MaxLength(8), Required(ErrorMessage = "Не указан пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
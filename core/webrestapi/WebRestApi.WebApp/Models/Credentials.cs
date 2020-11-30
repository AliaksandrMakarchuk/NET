using System.ComponentModel.DataAnnotations;

namespace WebRestApi.WebApp.Models {
    public class Credentials {
        [Required, MinLength (2), MaxLength (15)]
        public string Name { get; set; }

        [Required, MinLength (2), MaxLength (8)]
        public string Password { get; set; }
    }
}
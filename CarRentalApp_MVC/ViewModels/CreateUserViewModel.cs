using System.ComponentModel.DataAnnotations;

namespace CarRentalApp_MVC.ViewModels
{
    public class CreateUserViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }
    }
}

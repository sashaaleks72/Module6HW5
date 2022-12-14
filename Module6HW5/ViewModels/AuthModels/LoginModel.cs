using System.ComponentModel.DataAnnotations;

namespace Module6HW5.ViewModels.AuthModels
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Field must be filled!")]
        [EmailAddress(ErrorMessage = "Uncorrect email address!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Field must be filled!")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}

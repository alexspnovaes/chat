using System.ComponentModel.DataAnnotations;

namespace Chat.UI.Models
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password, ErrorMessage = "Invalid Password")]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirmation")]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
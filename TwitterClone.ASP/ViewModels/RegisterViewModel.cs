using System.ComponentModel.DataAnnotations;

namespace TwitterClone.ASP.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "The Email must not be empty")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Email length is Invalid")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "The Password must not be empty")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password length is Invalid")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Password Confirm must not be empty")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password Confirm length is Invalid")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        [Display(Name = "Password Confirm")]
        public string PasswordConfirm { get; set; }

        [Required(ErrorMessage = "The Alias must not be empty")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Alias length is Invalid")]        
        [Display(Name = "Alias")]
        public string Alias { get; set; }
    }
}

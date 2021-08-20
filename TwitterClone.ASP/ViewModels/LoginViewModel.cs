using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication;

namespace TwitterClone.ASP.ViewModels
{
    public class LoginViewModel
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

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}

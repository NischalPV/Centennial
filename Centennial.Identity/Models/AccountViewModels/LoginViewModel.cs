using System;
using System.ComponentModel.DataAnnotations;

namespace Centennial.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [Phone]
        public string Phone { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }
    }
}

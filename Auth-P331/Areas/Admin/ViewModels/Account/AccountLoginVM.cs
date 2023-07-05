using System.ComponentModel.DataAnnotations;

namespace Auth_P331.Areas.Admin.ViewModels.Account
{
    public class AccountLoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string? ReturnUrl { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace Auth_P331.Areas.Admin.ViewModels.Role
{
    public class RoleUpdateVM
    {
        [Required]
        public string Name { get; set; }
    }
}

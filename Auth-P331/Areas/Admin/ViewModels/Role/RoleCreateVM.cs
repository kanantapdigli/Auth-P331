using Microsoft.Build.Framework;

namespace Auth_P331.Areas.Admin.ViewModels.Role
{
    public class RoleCreateVM
    {
        [Required]
        public string Name { get; set; }
    }
}

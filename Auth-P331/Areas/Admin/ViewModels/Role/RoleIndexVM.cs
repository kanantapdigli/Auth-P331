using Microsoft.AspNetCore.Identity;

namespace Auth_P331.Areas.Admin.ViewModels.Role
{
    public class RoleIndexVM
    {
        public List<IdentityRole> Roles { get; set; }
    }
}

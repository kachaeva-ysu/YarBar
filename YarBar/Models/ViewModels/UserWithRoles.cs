using Microsoft.AspNetCore.Identity;

namespace YarBar.Models.ViewModels
{
    public class UserWithRoles
    {
        public IdentityUser User { get; set; }
        public IList<string> Roles { get; set; }
    }
}

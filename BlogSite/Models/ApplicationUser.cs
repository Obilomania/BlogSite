using Microsoft.AspNetCore.Identity;

namespace BlogSite.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string NickName { get; set; }
    }
}

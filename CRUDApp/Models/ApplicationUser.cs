using Microsoft.AspNetCore.Identity;

namespace CRUDApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}

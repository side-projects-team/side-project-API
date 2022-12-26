using Microsoft.AspNetCore.Identity;

namespace side_project_API.Entities.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}

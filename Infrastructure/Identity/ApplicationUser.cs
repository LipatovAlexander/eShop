using Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity;
public class ApplicationUser : IdentityUser<int>
{
    public Customer Customer { get; set; }
}

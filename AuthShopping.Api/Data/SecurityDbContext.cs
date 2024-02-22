using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AuthShopping.Data;

public class SecurityDbContext : IdentityDbContext<IdentityUser>
{
    public SecurityDbContext(DbContextOptions<SecurityDbContext> options) : base(options)
    { }
}
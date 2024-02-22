using Microsoft.EntityFrameworkCore;
using AuthShopping.Models;

namespace AuthShopping.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }

    public DbSet<ProductModel> Products { get; set; }
    public DbSet<CategoryModel> Categories { get; set; }
    public DbSet<ShoppingCartModel> ShoppingCarts { get; set; }
}
using AuthShopping.Data;
using AuthShopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthShopping.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductController(ILogger<ShoppingCartController> logger, ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<List<ProductModel>?> GetUsersProducts()
    {
        return await _applicationDbContext.Products.ToListAsync();
    }
}

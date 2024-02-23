using AuthShopping.Api.Data;
using AuthShopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthShopping.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class ShoppingCartController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ShoppingCartController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<List<ProductModel>> GetUsersProducts()
    {
        var user = User.Identity?.Name ?? string.Empty;

        var cart = await _applicationDbContext.ShoppingCarts.Where(cart => cart.User == user).FirstOrDefaultAsync();

        return cart?.Products is null ? ([]) : cart.Products;
    }

    [HttpPost]
    public async Task<IActionResult> DeleteUsersProduct(int id)
    {
        var user = User.Identity?.Name ?? string.Empty;

        var cart = await _applicationDbContext.ShoppingCarts.Where(cart => cart.User == user).FirstOrDefaultAsync();

        cart?.Products.RemoveAll(product => product.Id == id);

        await _applicationDbContext.SaveChangesAsync();

        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUsersProduct(int id)
    {
        var product = await _applicationDbContext.Products.FindAsync(id);

        // Check for null product
        if (product is null)
            return BadRequest(id);

        var user = User.Identity?.Name ?? string.Empty;

        var cart = await _applicationDbContext.ShoppingCarts.Where(cart => cart.User == user).FirstOrDefaultAsync();

        // Check for null shopping cart
        if (cart is null)
        {
            cart = new ShoppingCartModel()
            {
                User = user
            };
            cart.Products.Add(product);

            _applicationDbContext.Add(cart);
        }
        else
            cart.Products.Add(product);

        await _applicationDbContext.SaveChangesAsync();

        return Ok();
    }
}

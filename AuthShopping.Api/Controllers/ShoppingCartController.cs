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

        if (cart?.Products is null)
            return [];
        else
            return cart.Products;
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
        var user = User.Identity?.Name ?? string.Empty;

        var cart = await _applicationDbContext.ShoppingCarts.Where(cart => cart.User == user).FirstOrDefaultAsync();

        if (cart is null)
        {
            _applicationDbContext.Add(new ShoppingCartModel()
            {
                User = user,
                Products = [new ProductModel() { Id = id }]
            });
        }
        else
        {
            cart.Products.Add(new ProductModel() { Id = id });
        }

        await _applicationDbContext.SaveChangesAsync();

        return Ok();
    }
}

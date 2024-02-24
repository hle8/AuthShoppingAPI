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
    public async Task<IActionResult> GetProducts()
    {
        var userName = User.Identity?.Name ?? string.Empty;

        var cart = await _applicationDbContext.ShoppingCarts
        .Where(cart => cart.User == userName)
        .Include(cart => cart.Products)
        .ThenInclude(product => product.Category)
        .FirstOrDefaultAsync();

        if (cart is null)
            return Ok(new List<ProductModel>());

        return Ok(cart.Products.ToList());
    }

    [HttpPost]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var userName = User.Identity?.Name ?? string.Empty;

        var cart = await _applicationDbContext.ShoppingCarts
        .Where(cart => cart.User == userName)
        .Include(cart => cart.Products)
        .FirstOrDefaultAsync();

        var result = cart?.Products.RemoveAll(product => product.Id == id);

        await _applicationDbContext.SaveChangesAsync();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(int id)
    {
        var product = await _applicationDbContext.Products.FindAsync(id);

        // Check for null product
        if (product is null)
            return BadRequest($"Product:{id} not existing");

        var userName = User.Identity?.Name ?? string.Empty;

        var cart = await _applicationDbContext.ShoppingCarts.Where(cart => cart.User == userName).FirstOrDefaultAsync();

        // Check for null shopping cart
        if (cart is null)
        {
            cart = new ShoppingCartModel()
            {
                User = userName
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

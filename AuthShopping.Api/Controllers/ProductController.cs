using AuthShopping.Api.Data;
using AuthShopping.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthShopping.Api.Controllers;

[Authorize]
[ApiController]
[Route("[controller]/[action]")]
public class ProductController : ControllerBase
{
    private readonly ApplicationDbContext _applicationDbContext;

    public ProductController(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetProducts()
    {
        return Ok(await _applicationDbContext.Products.Include(product => product.Category).ToListAsync() ?? []);
    }

    [HttpGet]
    public async Task<List<ProductModel>> GetProductsByCategory(int id)
    {
        var category = await _applicationDbContext.Categories.Where(category => category.Id == id).FirstOrDefaultAsync();

        if (category is null)
            return [];
        else
            return await _applicationDbContext.Products.Where(product => product.Category == category).ToListAsync();
    }

    [HttpPost]
    public async Task<IActionResult> AddProduct(ProductModel newProduct)
    {
        // Check for duplicated product
        var product = await _applicationDbContext.Products.Where
        (product =>
            product.Name == newProduct.Name &&
            product.Price == newProduct.Price &&
            product.Description == newProduct.Description &&
            product.Category == newProduct.Category
        ).FirstOrDefaultAsync();

        if (product is null)
            // Create new product if not null
            _applicationDbContext.Add(newProduct);

        await _applicationDbContext.SaveChangesAsync();

        return Ok();
    }
}

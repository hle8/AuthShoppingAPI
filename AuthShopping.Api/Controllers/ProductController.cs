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
    public Task<List<ProductModel>> GetProducts()
    {
        return _applicationDbContext.Products.ToListAsync();
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
        var product = await _applicationDbContext.Products.FindAsync(newProduct);

        if (product is null)
            _applicationDbContext.Add(newProduct);
        else
        {
            product.Category = newProduct.Category;
            product.Description = newProduct.Description;
            product.Price = newProduct.Price;
            product.Name = newProduct.Name;
        }

        await _applicationDbContext.SaveChangesAsync();

        return Ok();
    }
}

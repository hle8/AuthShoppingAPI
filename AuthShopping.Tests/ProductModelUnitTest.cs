using AuthShopping.Models;

namespace AuthShopping.Tests;

public class ProductModelUnitTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void SetId(int id)
    {
        var category = new ProductModel()
        {
            Id = id
        };

        Assert.Equal(id, category.Id);
    }

    [Theory]
    [InlineData("100.00")]
    [InlineData("230.00")]
    [InlineData("1099.99")]
    [InlineData("1299.25")]
    [InlineData("0.99")]
    public void SetPrice(string strPrice)
    {
        var price = decimal.Parse(strPrice);

        var category = new ProductModel()
        {
            Price = price
        };

        Assert.Equal(price, category.Price);
    }

    [Theory]
    [InlineData("Pet")]
    [InlineData("Food")]
    [InlineData("Toy")]
    [InlineData("Music")]
    [InlineData("Smart Phone")]
    public void SetDescription(string description)
    {
        var category = new ProductModel()
        {
            Description = description
        };

        Assert.Equal(description, category.Description);
    }
}
using AuthShopping.Models;

namespace AuthShopping.Tests;

public class CategoryModelUnitTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void SetId(int id)
    {
        var category = new CategoryModel()
        {
            Id = id
        };

        Assert.Equal(id, category.Id);
    }

    [Theory]
    [InlineData("Pet")]
    [InlineData("Food")]
    [InlineData("Toy")]
    [InlineData("Music")]
    [InlineData("Smart Phone")]
    public void SetDescription(string description)
    {
        var category = new CategoryModel()
        {
            Description = description
        };

        Assert.Equal(description, category.Description);
    }
}
using AuthShopping.Models;

namespace AuthShopping.Tests;

public class ShoppingCartModelUnitTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    public void SetId(int id)
    {
        var category = new ShoppingCartModel()
        {
            Id = id
        };

        Assert.Equal(id, category.Id);
    }

    [Theory]
    [InlineData("name1@mycambrian.ca")]
    [InlineData("name2@mycambrian.ca")]
    [InlineData("huyle@gmail.com")]
    [InlineData("huyle1@gmail.com")]
    [InlineData("huy@yahoo.com")]
    public void SetUser(string email)
    {
        var category = new ShoppingCartModel()
        {
            User = email
        };

        Assert.Equal(email, category.User);
    }
}
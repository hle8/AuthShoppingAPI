namespace AuthShopping.Models;

public class ShoppingCartModel
{
    public int Id { get; set; }
    public string? User { get; set; }
    public List<ProductModel> Products { get; set; } = [];
}

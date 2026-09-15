amespace PR9.DTOs;

public class ProductDto
{
    public int ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public decimal Price { get; set; }

    public int Stock { get; set; }

    public int? CategoryId { get; set; }

    public DateTime CreatedAt { get; set; }
}

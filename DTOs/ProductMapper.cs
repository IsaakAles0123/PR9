sing PR9.Models;

namespace PR9.DTOs;

public static class ProductMapper
{
    public static ProductDto ToDto(Product product)
    {
        return new ProductDto
        {
            ProductId = product.ProductId,
            ProductName = product.ProductName,
            Price = product.Price,
            Stock = product.Stock,
            CategoryId = product.CategoryId,
            CreatedAt = product.CreatedAt
        };
    }
}

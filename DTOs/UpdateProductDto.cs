sing System.ComponentModel.DataAnnotations;

namespace PR9.DTOs;

public class UpdateProductDto
{
    [Required]
    [MaxLength(200)]
    public string ProductName { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Stock { get; set; }

    [Required]
    public int? CategoryId { get; set; }
}

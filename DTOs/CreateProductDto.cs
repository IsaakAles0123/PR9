using System.ComponentModel.DataAnnotations;

namespace PR9.DTOs;

public class CreateProductDto
{
    [Required]
    [MaxLength(200)]
    public string ProductName { get; set; } = null!;

    [Required]
    public decimal Price { get; set; }

    [Required]
    public int Stock { get; set; }

    public int? CategoryId { get; set; }
}

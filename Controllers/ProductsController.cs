using Microsoft.AspNetCore.Mvc;
using PR9.DTOs;
using PR9.Models;

namespace PR9.Controllers;

[ApiController]
[Route("api/products")]
public class ProductsController : ControllerBase
{
    private readonly SneakerShopContext _context;

    public ProductsController(SneakerShopContext context)
    {
        _context = context;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<ProductDto>> GetAll(int page = 1, int pageSize = 10)
    {
        if (page < 1)
        {
            page = 1;
        }

        if (pageSize < 1)
        {
            pageSize = 10;
        }

        var products = _context.Products
            .OrderBy(product => product.ProductId)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList()
            .Select(ProductMapper.ToDto)
            .ToList();

        return Ok(products);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProductDto> GetById(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        return Ok(ProductMapper.ToDto(product));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ProductDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<ProductDto> Create(CreateProductDto dto)
    {
        if (dto.CategoryId.HasValue && !_context.Categories.Any(category => category.CategoryId == dto.CategoryId.Value))
        {
            return NotFound();
        }

        var product = new Product
        {
            ProductName = dto.ProductName,
            Price = dto.Price,
            Stock = dto.Stock,
            CategoryId = dto.CategoryId,
            CreatedAt = DateTime.UtcNow
        };

        _context.Products.Add(product);
        _context.SaveChanges();

        var result = ProductMapper.ToDto(product);
        return CreatedAtAction(nameof(GetById), new { id = result.ProductId }, result);
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, UpdateProductDto dto)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        product.ProductName = dto.ProductName;
        product.Price = dto.Price;
        product.Stock = dto.Stock;
        product.CategoryId = dto.CategoryId;

        _context.SaveChanges();
        return NoContent();
    }

    [HttpPatch("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Patch(int id, PatchProductDto dto)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        if (dto.ProductName != null)
        {
            product.ProductName = dto.ProductName;
        }

        if (dto.Price != null)
        {
            product.Price = dto.Price.Value;
        }

        if (dto.Stock != null)
        {
            product.Stock = dto.Stock.Value;
        }

        if (dto.CategoryId != null)
        {
            product.CategoryId = dto.CategoryId;
        }

        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Delete(int id)
    {
        var product = _context.Products.Find(id);
        if (product == null)
        {
            return NotFound();
        }

        _context.Products.Remove(product);
        _context.SaveChanges();
        return NoContent();
    }
}

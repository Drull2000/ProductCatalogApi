using ProductCatalogApi.Data;
using ProductCatalogApi.Data.Models;
using ProductCatalogApi.DTO;
using ProductCatalogApi.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ProductController : ControllerBase
{
    private const string PhotoFolder = "products";
    private readonly AppDbContext _context;
    private readonly IFileService _fileService;

    public ProductController(AppDbContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreateProductDTO dto)
    {
        var product = new Product
        {
            Id = Guid.NewGuid(),
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CreatedAt = DateTime.UtcNow
        };

        for (var index = 0; index < dto.Photos.Count; index++)
        {
            var fileResult = await _fileService.SaveFileAsync(dto.Photos[index], PhotoFolder);
            product.Photos.Add(new ProductPhoto
            {
                Id = Guid.NewGuid(),
                Url = fileResult.RelativePath,
                FileName = fileResult.FileName,
                FileSize = fileResult.FileSize,
                SortOrder = index
            });
        }

        _context.Products.Add(product);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var product = await _context.Products
            .AsNoTracking()
            .Include(item => item.Photos.OrderBy(photo => photo.SortOrder))
            .FirstOrDefaultAsync(item => item.Id == id);

        return product is null ? NotFound() : Ok(product);
    }
}

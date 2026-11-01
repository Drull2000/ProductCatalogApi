using System.ComponentModel.DataAnnotations;

namespace ProductCatalogApi.DTO;

public class CreateProductDTO : IValidatableObject
{
    [Required, StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(2000)]
    public string Description { get; set; } = string.Empty;

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public List<IFormFile> Photos { get; set; } = new();

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Photos.Count > 10)
        {
            yield return new ValidationResult("A product can have no more than 10 photos.", new[] { nameof(Photos) });
        }

        var allowedExtensions = new[] { ".jpeg", ".jpg", ".png", ".webp" };
        const long maxFileSize = 5 * 1024 * 1024;
        foreach (var photo in Photos)
        {
            var extension = Path.GetExtension(photo.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
            {
                yield return new ValidationResult($"Unsupported image format: {extension}.", new[] { nameof(Photos) });
            }

            if (photo.Length > maxFileSize)
            {
                yield return new ValidationResult($"Photo '{photo.FileName}' must be smaller than 5 MB.", new[] { nameof(Photos) });
            }
        }
    }
}

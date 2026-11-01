using System.Text.Json.Serialization;

namespace ProductCatalogApi.Data.Models;

public class ProductPhoto
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string Url { get; set; } = string.Empty;
    public string FileName { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public int SortOrder { get; set; }
    [JsonIgnore]
    public Product Product { get; set; } = null!;
}

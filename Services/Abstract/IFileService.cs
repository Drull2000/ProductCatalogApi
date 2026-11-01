using ProductCatalogApi.DTO;

namespace ProductCatalogApi.Services.Abstract;
public interface IFileService
{
    Task<FileResponseDTO> SaveFileAsync(IFormFile file, string subFolder);
    void DeleteFile(string relativePath);
}

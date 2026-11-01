using ProductCatalogApi.Data;
using ProductCatalogApi.Services;
using ProductCatalogApi.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace ProductCatalogApi;
public class Program
{
    public static void Main(string[] args)
    {

        var builder = WebApplication.CreateBuilder(args);
        var connectionString = builder.Configuration.GetConnectionString("DefualtConnection");
        builder.Services.AddDbContext<AppDbContext>(option =>
        {
            option.UseNpgsql(connectionString);
        });
        builder.Services.AddScoped<IFileService, FileService>();

        // Add services to the container.

        builder.Services.AddControllers();
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new Microsoft.OpenApi.OpenApiInfo { Title = "ProductCatalogApi", Version = "v1" });
        });
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}

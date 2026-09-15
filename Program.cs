using Microsoft.EntityFrameworkCore;
using PR9.Models;

namespace PR9;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<SneakerShopContext>(options =>
            options.UseSqlServer(connectionString));

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<SneakerShopContext>();
            EnsureCreatedAtColumn(db);
        }

        app.UseSwagger();
        app.UseSwaggerUI();

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    private static void EnsureCreatedAtColumn(SneakerShopContext db)
    {
        db.Database.ExecuteSqlRaw(@"
IF COL_LENGTH('dbo.Products', 'CreatedAt') IS NULL
BEGIN
    ALTER TABLE dbo.Products
    ADD CreatedAt DATETIME2 NOT NULL
        CONSTRAINT DF_Products_CreatedAt DEFAULT (SYSUTCDATETIME());
END
");
    }
}

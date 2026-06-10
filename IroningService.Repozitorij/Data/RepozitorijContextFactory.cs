using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace IroningService.Repozitorij.Data
{
    public class RepozitorijContextFactory : IDesignTimeDbContextFactory<RepozitorijContext>
    {
        public RepozitorijContext CreateDbContext(string[] args)
        {
            // Učitaj konfiguraciju iz appsettings.json
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory() + "/../IroningService.API")
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<RepozitorijContext>();
            var connectionString = configuration.GetConnectionString("DefaultConnection"); // Provjeri je li ovo ime u tvom appsettings.json
            optionsBuilder.UseSqlServer(connectionString);

            return new RepozitorijContext(optionsBuilder.Options);
        }
    }
}
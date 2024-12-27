using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace BeestjeOpJeFeestje.Data.DbContext
{
    public class BeestjeOpJeFeestjeDbContextFactory : IDesignTimeDbContextFactory<BeestjeOpJeFeestjeDbContext>
    {
        public BeestjeOpJeFeestjeDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BeestjeOpJeFeestjeDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            
            var connectionString = configuration.GetConnectionString("MyConnection");

            optionsBuilder.UseSqlServer(connectionString);

            return new BeestjeOpJeFeestjeDbContext(optionsBuilder.Options);
        }
    }
}
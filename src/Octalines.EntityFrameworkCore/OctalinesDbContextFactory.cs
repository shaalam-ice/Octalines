using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Octalines.EntityFrameworkCore;

public class OctalinesDbContextFactory : IDesignTimeDbContextFactory<OctalinesDbContext>
{
    public OctalinesDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OctalinesDbContext>();
        optionsBuilder.UseSqlServer("Server=localhost;Database=Octalines;Trusted_Connection=True;TrustServerCertificate=True");
        return new OctalinesDbContext(optionsBuilder.Options);
    }
}

using IroningService.Repozitorij.Data;
using Microsoft.EntityFrameworkCore;

public abstract class IntegrationTestBase
{
    protected RepozitorijContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<RepozitorijContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Svaki test dobiva novu bazu
            .Options;

        var context = new RepozitorijContext(options);
        context.Database.EnsureCreated();
        return context;
    }
}
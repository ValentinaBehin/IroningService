using Xunit;
using Microsoft.EntityFrameworkCore;
using IroningService.Repozitorij.Repozitoriji; // Provjeri namespace gdje ti je UslugaRepozitorij
using IroningService.Repozitorij.Data;
using IroningService.Domena.Entiteti;

namespace IroningService.Testovi;

public class UslugaRepozitorijTests
{
    // Pomoćna metoda za kreiranje svježe baze za svaki test
    private RepozitorijContext KreirajInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<RepozitorijContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // Svaki test dobije svoju bazu
            .Options;
        return new RepozitorijContext(options);
    }

    [Fact]
    public async Task GetByIdAsync_TrebaDohvatitiIspravnuUslugu()
    {
        // ARRANGE
        var context = KreirajInMemoryContext();
        var ocekivanaUsluga = new UslugaPeglanja { Id = 1, Naziv = "Peglanje Košulje", Cijena = 2.50m };
        context.Usluge.Add(ocekivanaUsluga);
        await context.SaveChangesAsync();

        var repo = new UslugaRepozitorij(context);

        // ACT
        var rezultat = await repo.GetByIdAsync(1);

        // ASSERT
        Assert.NotNull(rezultat);
        Assert.Equal("Peglanje Košulje", rezultat.Naziv);
        Assert.Equal(2.50m, rezultat.Cijena);
    }
}
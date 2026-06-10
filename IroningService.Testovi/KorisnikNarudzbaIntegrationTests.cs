using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Repozitoriji;
using IroningService.Servis.Implementacije;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace IroningService.Testovi;

public class KorisnikNarudzbaIntegrationTests
{
    // Pomoćna metoda za kreiranje svježe In-Memory baze za svaki test
    private RepozitorijContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<RepozitorijContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new RepozitorijContext(options);
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task KreirajNarudzbuZaKorisnika_ValidniPodaci_SpremaNarudzbuPovezanuSKorisnikom()
    {
        // 1. ARRANGE (Priprema)
        using var context = CreateInMemoryContext();
        
        // Inicijalizacija repozitorija
        var repoUsluga = new UslugaRepozitorij(context);
        var repoNarudzba = new NarudzbaRepozitorij(context);
        
        // Inicijalizacija servisa (prilagodi broj argumenata ako tvoj konstruktor traži i context)
        var servisNarudzba = new NarudzbaServis(repoUsluga, repoNarudzba);

        // Kreiranje korisnika i spremanje u bazu
        var korisnik = new Korisnik 
        { 
            Ime = "Pero", 
            Prezime = "Perić", 
            Email = "pero@test.hr", 
            Lozinka = "1234567" 
        };
        context.Korisnici.Add(korisnik);
        await context.SaveChangesAsync();

        // Kreiranje narudžbe
        var narudzba = new Narudzba { KorisnikId = korisnik.Id, UkupnaCijena = 50 };

        // 2. ACT (Radnja)
        await servisNarudzba.KreirajNarudzbu(narudzba);

        // 3. ASSERT (Provjera)
        var spremljenaNarudzba = await context.Narudzbe.FirstOrDefaultAsync(n => n.KorisnikId == korisnik.Id);
        
        Assert.NotNull(spremljenaNarudzba);
        Assert.Equal(50, spremljenaNarudzba.UkupnaCijena);
        Assert.Equal(korisnik.Id, spremljenaNarudzba.KorisnikId);
    }
}
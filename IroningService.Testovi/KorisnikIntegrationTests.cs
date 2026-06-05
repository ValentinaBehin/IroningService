using IroningService.Repozitorij.Repozitoriji;
using IroningService.Servis.Implementacije;
using IroningService.Domena.Entiteti;
using Xunit;

public class KorisnikIntegrationTests : IntegrationTestBase
{
    [Fact]
    public async Task RegistracijaIPreuzimanjeKorisnika_TrebaRaditi()
    {
        // 1. ARRANGE (Priprema)
        using var context = CreateInMemoryContext();
        var repo = new KorisnikRepozitorij(context);
        var servis = new KorisnikServis(repo); // Koristimo pravu implementaciju, ne mock!

        var noviKorisnik = new Korisnik { 
            Email = "integracija@test.hr", 
            Ime = "Pero", 
            Lozinka = "sigurnaLozinka123" 
        };

        // 2. ACT (Izvršenje)
        await servis.RegistrirajKorisnikaAsync(noviKorisnik);

        // 3. ASSERT (Provjera)
        var dohvaceniKorisnik = await repo.GetByEmailAsync("integracija@test.hr");
        
        Assert.NotNull(dohvaceniKorisnik);
        Assert.Equal("Pero", dohvaceniKorisnik.Ime);
        Assert.Equal("integracija@test.hr", dohvaceniKorisnik.Email);
    }
}
using Xunit;
using Moq;
using IroningService.Servis.Implementacije;
using IroningService.Repozitorij.Suclja;
using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Data; // Provjeri da li je ovo točan namespace za tvoj RepozitorijContext

namespace IroningService.Testovi;

public class NarudzbaServisTests
{
    [Fact]
    public async Task IzracunajUkupnuCijenu_TrebaZbrojitiStavke()
    {
        // 1. ARRANGE (Priprema)
        var mockUslugaRepo = new Mock<IUslugaRepozitorij>();
        var mockNarudzbaRepo = new Mock<INarudzbaRepozitorij>();
        
        // Stvaramo mock za kontekst kako bismo izbjegli null upozorenje
        var mockContext = new Mock<RepozitorijContext>(); 

        // Postavljamo ponašanje mock repozitorija
        mockUslugaRepo.Setup(repo => repo.GetByIdAsync(1))
                      .ReturnsAsync(new UslugaPeglanja { Id = 1, Naziv = "Peglanje", Cijena = 10.00m });

        // Inicijaliziramo servis s .Object (to pretvara Mock u pravi objekt)
        var servis = new NarudzbaServis(
            mockUslugaRepo.Object, 
            mockNarudzbaRepo.Object, 
            mockContext.Object
        );

        var stavke = new List<StavkaNarudzbe>
        {
            new StavkaNarudzbe { UslugaId = 1, Kolicina = 3 }
        };

        // 2. ACT (Izvršavanje)
        var ukupno = await servis.IzracunajUkupnuCijenu(stavke);

        // 3. ASSERT (Provjera)
        Assert.Equal(30.00m, ukupno);
    }
}
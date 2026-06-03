using Xunit;
using Moq;
using IroningService.Servis.Implementacije;
using IroningService.Repozitorij.Suclja;
using IroningService.Domena.Entiteti;

namespace IroningService.Testovi;

public class NarudzbaServisTests
{
    [Fact]
    public async Task IzracunajUkupnuCijenu_TrebaZbrojitiStavke()
    {
        // ARRANGE
        var mockUslugaRepo = new Mock<IUslugaRepozitorij>();
        var mockNarudzbaRepo = new Mock<INarudzbaRepozitorij>();
        
        // KORISTIMO KONSTRUKTOR S DVA PARAMETRA (bez contexta)
        var servis = new NarudzbaServis(mockUslugaRepo.Object, mockNarudzbaRepo.Object);

        mockUslugaRepo.Setup(repo => repo.GetByIdAsync(1))
                      .ReturnsAsync(new UslugaPeglanja { Id = 1, Naziv = "Peglanje", Cijena = 10.00m });

        var stavke = new List<StavkaNarudzbe>
        {
            new StavkaNarudzbe { UslugaId = 1, Kolicina = 3 }
        };

        // ACT
        var ukupno = await servis.IzracunajUkupnuCijenu(stavke);

        // ASSERT
        Assert.Equal(30.00m, ukupno);
    }

    [Fact]
    public async Task IzracunajUkupnuCijenu_PraznaLista_TrebaVratitiNulu()
    {
        // ARRANGE
        var mockUslugaRepo = new Mock<IUslugaRepozitorij>();
        var mockNarudzbaRepo = new Mock<INarudzbaRepozitorij>();

        var servis = new NarudzbaServis(mockUslugaRepo.Object, mockNarudzbaRepo.Object);
        var prazneStavke = new List<StavkaNarudzbe>();

        // ACT
        var ukupno = await servis.IzracunajUkupnuCijenu(prazneStavke);

        // ASSERT
        Assert.Equal(0, ukupno);
    }

    [Fact]
    public async Task IzracunajUkupnuCijenu_RazliciteStavke_TrebaZbrojitiTocno()
    {
        // ARRANGE
        var mockUslugaRepo = new Mock<IUslugaRepozitorij>();
        var mockNarudzbaRepo = new Mock<INarudzbaRepozitorij>();

        mockUslugaRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new UslugaPeglanja { Id = 1, Cijena = 10.00m });
        mockUslugaRepo.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(new UslugaPeglanja { Id = 2, Cijena = 5.00m });

        var servis = new NarudzbaServis(mockUslugaRepo.Object, mockNarudzbaRepo.Object);
        var stavke = new List<StavkaNarudzbe>
        {
            new StavkaNarudzbe { UslugaId = 1, Kolicina = 1 }, 
            new StavkaNarudzbe { UslugaId = 2, Kolicina = 2 }  
        };

        // ACT
        var ukupno = await servis.IzracunajUkupnuCijenu(stavke);

        // ASSERT
        Assert.Equal(20.00m, ukupno);
    }
}
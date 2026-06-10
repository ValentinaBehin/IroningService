using Xunit;
using Moq;
using IroningService.Servis.Implementacije;
using IroningService.Repozitorij.Suclja;
using IroningService.Domena.Entiteti;

namespace IroningService.Testovi;

public class KorisnikServisTests
{
    private readonly Mock<IKorisnikRepozitorij> _mockKorisnikRepo;
    private readonly KorisnikServis _servis;

    public KorisnikServisTests()
    {
        // Priprema zajedničkih objekata za sve testove u ovoj klasi
        _mockKorisnikRepo = new Mock<IKorisnikRepozitorij>();
        _servis = new KorisnikServis(_mockKorisnikRepo.Object);
    }

    [Fact]
    public async Task DohvatiKorisnikaAsync_KorisnikPostoji_VracaKorisnika()
    {
        // ARRANGE
        var email = "test@primjer.hr";
        var ocekivaniKorisnik = new Korisnik { Id = 1, Email = email, Ime = "Marko" };
        
        _mockKorisnikRepo.Setup(r => r.GetByEmailAsync(email))
                         .ReturnsAsync(ocekivaniKorisnik);

        // ACT
        var rezultat = await _servis.DohvatiKorisnikaAsync(email);

        // ASSERT
        Assert.NotNull(rezultat);
        Assert.Equal("Marko", rezultat.Ime);
    }

    [Fact]
    public async Task DohvatiKorisnikaAsync_KorisnikNePostoji_VracaNull()
    {
        // ARRANGE
        _mockKorisnikRepo.Setup(r => r.GetByEmailAsync(It.IsAny<string>()))
                         .ReturnsAsync((Korisnik)null!);

        // ACT
        var rezultat = await _servis.DohvatiKorisnikaAsync("nepostojeci@email.hr");

        // ASSERT
        Assert.Null(rezultat);
    }

    [Fact]
    public async Task RegistrirajKorisnikaAsync_PrekratkaLozinka_BacaIznimku()
    {
        // ARRANGE
        var noviKorisnik = new Korisnik { Email = "novi@test.hr", Lozinka = "123" };

        // ACT & ASSERT
        await Assert.ThrowsAsync<ArgumentException>(() => _servis.RegistrirajKorisnikaAsync(noviKorisnik));
    }
}
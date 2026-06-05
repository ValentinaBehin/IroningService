using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Suclja;

namespace IroningService.Servis.Implementacije;

public class KorisnikServis : IKorisnikServis
{
    private readonly IKorisnikRepozitorij _repo;
    public KorisnikServis(IKorisnikRepozitorij repo) => _repo = repo;
    public async Task<Korisnik?> DohvatiKorisnikaAsync(string email)
{
    // Ovdje koristimo repozitorij koji smo ubrizgali kroz konstruktor
    return await _repo.GetByEmailAsync(email);
}
public async Task RegistrirajKorisnikaAsync(Korisnik korisnik)
{
    // 1. Logička validacija (provjera baze)
    var postojeci = await _repo.GetByEmailAsync(korisnik.Email);
    if (postojeci != null)
    {
        throw new InvalidOperationException("Korisnik s ovim emailom već postoji.");
    }

    // 2. Dodatna pravila
    if (korisnik.Lozinka.Contains("123456")) 
    {
        throw new ArgumentException("Lozinka je prejednostavna.");
    }
    if (korisnik.Lozinka == null || korisnik.Lozinka.Length < 6)
    {
        throw new ArgumentException("Lozinka je prekratka!");
    }

    await _repo.DodajKorisnikaAsync(korisnik);
}
   
public async Task RegistrirajKorisnika(Korisnik korisnik) 
{
    await RegistrirajKorisnikaAsync(korisnik);
    await _repo.DodajKorisnikaAsync(korisnik);
}
    public async Task<Korisnik?> Prijava(string email, string lozinka) => await _repo.ProvjeriPrijavu(email, lozinka);
}
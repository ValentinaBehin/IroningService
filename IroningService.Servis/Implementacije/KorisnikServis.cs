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
    // Ovdje dodajemo logiku: npr. provjera lozinke
    if (korisnik.Lozinka.Length < 6)
    {
        throw new ArgumentException("Lozinka je prekratka!");
    }

    await _repo.DodajKorisnikaAsync(korisnik);
}
    public async Task RegistrirajKorisnika(Korisnik korisnik) => await _repo.DodajKorisnikaAsync(korisnik);
    public async Task<Korisnik?> Prijava(string email, string lozinka) => await _repo.ProvjeriPrijavu(email, lozinka);
}
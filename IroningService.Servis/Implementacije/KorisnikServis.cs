using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Suclja;

namespace IroningService.Servis.Implementacije;

public class KorisnikServis : IKorisnikServis
{
    private readonly IKorisnikRepozitorij _repo;
    public KorisnikServis(IKorisnikRepozitorij repo) => _repo = repo;

    public async Task RegistrirajKorisnika(Korisnik korisnik) => await _repo.DodajKorisnikaAsync(korisnik);
    public async Task<Korisnik?> Prijava(string email, string lozinka) => await _repo.ProvjeriPrijavu(email, lozinka);
}
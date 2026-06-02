using IroningService.Domena.Entiteti;

namespace IroningService.Servis.Suclja;

public interface IKorisnikServis
{
    Task RegistrirajKorisnika(Korisnik korisnik);
    Task<Korisnik?> Prijava(string email, string lozinka);
}
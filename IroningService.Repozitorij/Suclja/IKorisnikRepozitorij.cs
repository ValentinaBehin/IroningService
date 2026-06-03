using IroningService.Domena.Entiteti;

namespace IroningService.Repozitorij.Suclja;

public interface IKorisnikRepozitorij
{
    Task DodajKorisnikaAsync(Korisnik korisnik);
    Task<Korisnik?> ProvjeriPrijavu(string email, string lozinka);
    Task<Korisnik?> GetByEmailAsync(string email);
}
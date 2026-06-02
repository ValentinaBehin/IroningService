using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Suclja;
using Microsoft.EntityFrameworkCore;

namespace IroningService.Repozitorij.Repozitoriji;

public class KorisnikRepozitorij : IKorisnikRepozitorij
{
    private readonly RepozitorijContext _context;

    public KorisnikRepozitorij(RepozitorijContext context) => _context = context;

    public async Task DodajKorisnikaAsync(Korisnik korisnik)
    {
        _context.Korisnici.Add(korisnik);
        await _context.SaveChangesAsync();
    }

    public async Task<Korisnik?> ProvjeriPrijavu(string email, string lozinka)
    {
        return await _context.Korisnici
            .FirstOrDefaultAsync(k => k.Email == email && k.Lozinka == lozinka);
    }
}
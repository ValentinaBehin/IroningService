using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Suclja;
using Microsoft.EntityFrameworkCore;

namespace IroningService.Repozitorij.Repozitoriji;

public class KorisnikRepozitorij : IKorisnikRepozitorij
{
    private readonly RepozitorijContext _context;

    public KorisnikRepozitorij(RepozitorijContext context) 
    {
        _context = context;
    }

    public async Task<Korisnik?> GetByEmailAsync(string email)
    {
        return await _context.Korisnici.FirstOrDefaultAsync(k => k.Email == email);
    }

    public async Task DodajKorisnikaAsync(Korisnik korisnik)
    {
        _context.Entry(korisnik).Property(x => x.Id).IsModified = false;
        _context.Korisnici.Add(korisnik);
        await _context.SaveChangesAsync();
    }

    public async Task<Korisnik?> ProvjeriPrijavu(string email, string lozinka)
{
    // 1. Prvo dohvatimo korisnika samo po emailu (bez lozinke)
    var korisnik = await _context.Korisnici
        .FirstOrDefaultAsync(k => k.Email == email);

    if (korisnik == null) return null;

    // 2. Ručno usporedimo lozinku koristeći Trim() na onome što je izvučeno iz baze
    if (korisnik.Lozinka.Trim() == lozinka.Trim())
    {
        return korisnik;
    }

    return null;
}
}
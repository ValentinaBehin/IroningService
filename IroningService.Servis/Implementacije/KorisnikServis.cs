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
        return await _repo.GetByEmailAsync(email);
    }

    public async Task RegistrirajKorisnikaAsync(Korisnik korisnik)
    {
        // 1. Resetiraj Id na 0
        korisnik.Id = 0;

        // OČIŠĆAVANJE: Ukloni razmake s lozinke da se izbjegne problem s prijavom
        if (!string.IsNullOrEmpty(korisnik.Lozinka))
        {
            korisnik.Lozinka = korisnik.Lozinka.Trim();
        }

        // 2. Logička validacija
        var postojeci = await _repo.GetByEmailAsync(korisnik.Email);
        if (postojeci != null)
        {
            throw new InvalidOperationException("Korisnik s ovim emailom već postoji.");
        }

        // 3. Sigurnosna pravila
        if (string.IsNullOrEmpty(korisnik.Lozinka) || korisnik.Lozinka.Length < 6)
        {
            throw new ArgumentException("Lozinka je prekratka!");
        }

        if (korisnik.Lozinka.Contains("123456")) 
        {
            throw new ArgumentException("Lozinka je prejednostavna.");
        }

        // 4. Spremanje u bazu
        await _repo.DodajKorisnikaAsync(korisnik);
    }

    public async Task RegistrirajKorisnika(Korisnik korisnik) 
    {
        try 
        {
            await RegistrirajKorisnikaAsync(korisnik);
            Console.WriteLine($"Uspješno spremljen korisnik: {korisnik.Ime}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"!!! GREŠKA PRI REGISTRACIJI: {ex.Message}");
            throw; 
        }
    }

    public async Task<Korisnik?> Prijava(string? email, string? lozinka) 
{
    if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(lozinka)) 
        return null;

    string cistiEmail = email.Trim();
    string cistaLozinka = lozinka.Trim();

    var korisnik = await _repo.GetByEmailAsync(cistiEmail);
    
    if (korisnik != null) 
    {
        // OVO JE DEBUG ISPIS - GLEDAJ TERMINAL API-JA
        Console.WriteLine($"DEBUG: U bazi je lozinka: '{korisnik.Lozinka}', Duljina: {korisnik.Lozinka.Length}");
        Console.WriteLine($"DEBUG: Ti šalješ: '{cistaLozinka}', Duljina: {cistaLozinka.Length}");
        
        if (korisnik.Lozinka.Trim() == cistaLozinka)
            return korisnik;
    }
    else 
    {
        Console.WriteLine($"DEBUG: Korisnik s emailom '{cistiEmail}' nije pronađen u bazi.");
    }
    
    return null;
}
}
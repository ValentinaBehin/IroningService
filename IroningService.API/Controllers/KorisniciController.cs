using Microsoft.AspNetCore.Mvc;
using IroningService.Domena.Entiteti;
using IroningService.Servis.Suclja;

namespace IroningService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KorisniciController : ControllerBase
{
    private readonly IKorisnikServis _servis;

    public KorisniciController(IKorisnikServis servis) => _servis = servis;

    // Ispravna ruta: POST api/korisnici/registriraj
    [HttpPost("registriraj")]
    public async Task<IActionResult> Registriraj([FromBody] Korisnik korisnik)
    {
        try
        {
            await _servis.RegistrirajKorisnika(korisnik);
            return Ok(new { poruka = "Uspješna registracija" });
        }
        catch (Exception ex)
        {
            // Ovdje šaljemo poruku koju WPF može pročitati
            return BadRequest(new { poruka = ex.Message });
        }
    }

[HttpPost("prijava")]
public async Task<IActionResult> Prijava([FromBody] PrijavaModel podaci)
{
    // LOGIRANJE: Ovo ćeš vidjeti u terminalu API-ja
    Console.WriteLine($"API PRIMIO: Email={podaci.Email}, Lozinka={podaci.Lozinka}");
    
    var korisnik = await _servis.Prijava(podaci.Email, podaci.Lozinka);
    
    if (korisnik == null) 
    {
        Console.WriteLine("API: Korisnik nije pronađen.");
        return BadRequest("Pogrešan email ili lozinka.");
    }
    
    return Ok(korisnik);
}
}
public class PrijavaModel
{
    public string Email { get; set; } = string.Empty;
    public string Lozinka { get; set; } = string.Empty;
}
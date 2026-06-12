using Microsoft.AspNetCore.Mvc;
using IroningService.Domena.Entiteti;
using IroningService.Servis.Suclja;

namespace IroningService.API.Controllers;

[ApiController]
[Route("api/[controller]")] // Osnovna ruta je /api/korisnici
public class KorisniciController : ControllerBase
{
    private readonly IKorisnikServis _servis;

    public KorisniciController(IKorisnikServis servis)
    {
        _servis = servis;
    }

    // POST api/korisnici/registriraj
    [HttpPost("registriraj")]
    public async Task<IActionResult> Registriraj([FromBody] Korisnik korisnik)
    {
        if (korisnik == null) return BadRequest("Podaci korisnika nisu ispravni.");

        try
        {
            await _servis.RegistrirajKorisnika(korisnik);
            return Ok(new { poruka = "Uspješna registracija!" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { poruka = ex.Message });
        }
    }

    // POST api/korisnici/prijava
    [HttpPost("prijava")]
    public async Task<IActionResult> Prijava([FromBody] PrijavaModel podaci)
    {
        if (string.IsNullOrEmpty(podaci.Email) || string.IsNullOrEmpty(podaci.Lozinka))
            return BadRequest("Email i lozinka su obavezni.");

        var korisnik = await _servis.Prijava(podaci.Email, podaci.Lozinka);
        
        if (korisnik == null) 
        {
            return Unauthorized(new { poruka = "Pogrešan email ili lozinka." });
        }
        
        return Ok(korisnik);
    }
}

// Model za prijavu
public class PrijavaModel
{
    public string Email { get; set; } = string.Empty;
    public string Lozinka { get; set; } = string.Empty;
}
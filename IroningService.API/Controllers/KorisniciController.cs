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

    [HttpPost("registracija")]
    public async Task<IActionResult> Registriraj([FromBody] Korisnik korisnik)
    {
        await _servis.RegistrirajKorisnika(korisnik);
        return Ok(new { poruka = "Registracija uspješna!" });
    }

    [HttpPost("prijava")]
    public async Task<IActionResult> Prijava([FromBody] Korisnik podaci)
    {
        var korisnik = await _servis.Prijava(podaci.Email, podaci.Lozinka);
        if (korisnik == null) return Unauthorized("Pogrešni podaci!");
        return Ok(korisnik);
    }
}
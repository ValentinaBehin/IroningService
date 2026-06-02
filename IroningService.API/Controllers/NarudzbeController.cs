using Microsoft.AspNetCore.Mvc;
using IroningService.Domena.Entiteti;
using IroningService.Servis.Suclja;

namespace IroningService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NarudzbeController : ControllerBase
{
    private readonly INarudzbaServis _narudzbaServis;

    public NarudzbeController(INarudzbaServis narudzbaServis)
    {
        _narudzbaServis = narudzbaServis;
    }

    // GET: api/narudzbe?email=korisnik@example.com
    // Sada filtriramo narudžbe prema emailu
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Narudzba>>> GetPoEmailu([FromQuery] string email)
    {
        if (string.IsNullOrEmpty(email))
        {
            return BadRequest("Email je obavezan za dohvat narudžbi.");
        }

        try
        {
            // Ovdje koristimo servis da filtriramo narudžbe
            var narudzbe = await _narudzbaServis.DohvatiNarudzbePoEmailuAsync(email);
            return Ok(narudzbe);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Pogreška na serveru: {ex.Message}");
        }
    }

    // POST: api/narudzbe
    [HttpPost]
    public async Task<IActionResult> Kreiraj([FromBody] Narudzba narudzba)
    {
        if (narudzba == null)
        {
            return BadRequest("Narudžba ne smije biti prazna.");
        }

        try
        {
            await _narudzbaServis.KreirajNarudzbu(narudzba);
            return Ok(new { poruka = "Narudžba je uspješno zaprimljena!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Pogreška pri spremanju: {ex.Message}");
        }
    }
}
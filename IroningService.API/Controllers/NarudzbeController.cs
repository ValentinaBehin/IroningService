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

    // GET: api/narudzbe?email=...
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Narudzba>>> GetPoEmailu([FromQuery] string email)
    {
        if (string.IsNullOrEmpty(email)) return BadRequest("Email je obavezan.");

        try
        {
            var narudzbe = await _narudzbaServis.DohvatiNarudzbePoEmailuAsync(email);
            return Ok(narudzbe);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    // GET: api/narudzbe/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Narudzba>> GetNarudzbaPoId(int id)
    {
        var narudzba = await _narudzbaServis.DohvatiNarudzbuPoIdAsync(id);
        if (narudzba == null) return NotFound();
        return Ok(narudzba);
    }

    // GET: api/narudzbe/paginirano?korisnikId=...&pageNumber=...&pageSize=...
    [HttpGet("paginirano")]
    public async Task<IActionResult> GetPaginirano([FromQuery] int korisnikId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
    {
        var narudzbe = await _narudzbaServis.DohvatiSvePaginirano(korisnikId, pageNumber, pageSize);
        return Ok(narudzbe);
    }

    // POST: api/narudzbe
    [HttpPost]
    public async Task<IActionResult> Kreiraj([FromBody] Narudzba narudzba)
    {
        if (narudzba == null) return BadRequest("Narudžba ne smije biti prazna.");

        try
        {
            await _narudzbaServis.KreirajNarudzbu(narudzba);
            return Ok(new { poruka = "Uspješno!" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }
}
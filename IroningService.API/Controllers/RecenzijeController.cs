using Microsoft.AspNetCore.Mvc;
using IroningService.Domena.Entiteti;
using IroningService.Servis.Suclja;

namespace IroningService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RecenzijeController : ControllerBase
{
    private readonly IRecenzijaServis _recenzijaServis;
    private readonly ILogger<RecenzijeController> _logger; // Dodaj logger

    public RecenzijeController(IRecenzijaServis recenzijaServis, ILogger<RecenzijeController> logger)
    {
        _recenzijaServis = recenzijaServis;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> DodajRecenziju([FromBody] RecenzijaModel recenzija)
    {
        // 1. Logiraj da je zahtjev stigao
        _logger.LogInformation("Primljen zahtjev za spremanje recenzije za narudžbu: {Id}", recenzija?.NarudzbaId);

        if (recenzija == null) 
            return BadRequest("Podaci recenzije su prazni.");

        try
        {
            await _recenzijaServis.DodajRecenziju(recenzija);
            return Ok(new { poruka = "Recenzija uspješno spremljena!" });
        }
        catch (Exception ex)
        {
            // 2. Logiraj grešku u server konzoli
            _logger.LogError(ex, "Greška pri spremanju recenzije.");
            return StatusCode(500, new { detalji = ex.Message });
        }
    }
}
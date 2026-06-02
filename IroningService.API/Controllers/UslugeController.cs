using IroningService.Servis.Suclja;
using Microsoft.AspNetCore.Mvc;

namespace IroningService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UslugeController : ControllerBase
{
    private readonly IUslugaServis _servis;
    public UslugeController(IUslugaServis servis)
    {
        _servis = servis;
    }

    [HttpGet]
    public async Task<IActionResult> DohvatiSve()
    {
        return Ok(await _servis.DohvatiSveUsluge());
    }
}
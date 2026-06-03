using IroningService.Repozitorij.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IroningService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UslugeController : ControllerBase
{
    private readonly RepozitorijContext _context;

    public UslugeController(RepozitorijContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> DohvatiSveUsluge()
    {
        var usluge = await _context.Usluge.ToListAsync();
        return Ok(usluge);
    }
}
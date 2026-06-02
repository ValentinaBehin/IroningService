using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Suclja;
using Microsoft.EntityFrameworkCore;

namespace IroningService.Repozitorij.Repozitoriji;

public class UslugaRepozitorij : IUslugaRepozitorij
{
    private readonly RepozitorijContext _context;

    public UslugaRepozitorij(RepozitorijContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<UslugaPeglanja>> DohvatiSveUslugeAsync()
    {
        return await _context.Usluge.ToListAsync();
    }
}
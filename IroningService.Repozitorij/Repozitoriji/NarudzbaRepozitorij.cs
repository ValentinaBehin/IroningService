using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Suclja;
using Microsoft.EntityFrameworkCore; 

namespace IroningService.Repozitorij.Repozitoriji;

public class NarudzbaRepozitorij : INarudzbaRepozitorij
{
    private readonly RepozitorijContext _context;

    public NarudzbaRepozitorij(RepozitorijContext context)
    {
        _context = context;
    }

    public async Task DodajNarudzbuAsync(Narudzba narudzba)
    {
        await _context.Narudzbe.AddAsync(narudzba);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Narudzba>> GetAllAsync()
    {
        return await _context.Narudzbe.ToListAsync();
    }
}
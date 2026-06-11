using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Suclja;
using Microsoft.EntityFrameworkCore;

namespace IroningService.Repozitorij.Repozitoriji;

public class RecenzijaRepozitorij : IRecenzijaRepozitorij
{
    private readonly RepozitorijContext _context;

    public RecenzijaRepozitorij(RepozitorijContext context)
    {
        _context = context;
    }

    public async Task DodajRecenzijuAsync(RecenzijaModel recenzija) // OVDJE MORA BITI REZENZIJAMODEL
    {
        await _context.Recenzije.AddAsync(recenzija);
    }

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<List<RecenzijaModel>> DohvatiPoNarudzbiAsync(int narudzbaId) // OVDJE MORA BITI REZENZIJAMODEL
    {
        return await _context.Recenzije
            .Where(r => r.NarudzbaId == narudzbaId)
            .ToListAsync();
    }
}
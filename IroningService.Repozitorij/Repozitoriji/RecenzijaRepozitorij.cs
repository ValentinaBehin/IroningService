using IroningService.Repozitorij.Data;
using IroningService.Repozitorij.Suclja;
using IroningService.Domena.Entiteti;
using Microsoft.EntityFrameworkCore;

namespace IroningService.Repozitorij.Repozitoriji
{
    public class RecenzijaRepozitorij : IRecenzijaRepozitorij
    {
        private readonly RepozitorijContext _context;

        public RecenzijaRepozitorij(RepozitorijContext context)
        {
            _context = context;
        }

        public async Task DodajRecenzijuAsync(Recenzija recenzija)
        {
            await _context.Recenzije.AddAsync(recenzija);
            await _context.SaveChangesAsync();
        }
 public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<List<Recenzija>> DohvatiPoNarudzbiAsync(int narudzbaId)
        {
            return await _context.Recenzije
                .Where(r => r.NarudzbaId == narudzbaId)
                .ToListAsync();
        }
    }
}
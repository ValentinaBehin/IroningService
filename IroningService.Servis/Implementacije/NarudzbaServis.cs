using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Suclja;
using IroningService.Repozitorij.Data; // Ovdje se nalazi RepozitorijContext
using Microsoft.EntityFrameworkCore;

namespace IroningService.Servis.Implementacije;

public class NarudzbaServis : INarudzbaServis
{
    private readonly IUslugaRepozitorij _uslugaRepo;
    private readonly INarudzbaRepozitorij _narudzbaRepo;
    private readonly RepozitorijContext _context; // Ispravljeno ime klase

    public NarudzbaServis(IUslugaRepozitorij uslugaRepo, INarudzbaRepozitorij narudzbaRepo, RepozitorijContext context)
    {
        _uslugaRepo = uslugaRepo;
        _narudzbaRepo = narudzbaRepo;
        _context = context;
    }

    public async Task<IEnumerable<Narudzba>> DohvatiSveNarudzbeAsync()
    {
        return await _narudzbaRepo.GetAllAsync();
    }

    public async Task<List<Narudzba>> DohvatiNarudzbePoEmailuAsync(string email)
    {
        return await _context.Narudzbe
            .Include(n => n.Stavke)
            .Where(n => n.KlijentEmail == email)
            .OrderByDescending(n => n.DatumNarudzbe)
            .ToListAsync();
    }

    public async Task<decimal> IzracunajUkupnuCijenu(List<StavkaNarudzbe> stavke)
    {
        decimal ukupno = 0;
        foreach (var stavka in stavke)
        {
            var usluga = await _uslugaRepo.GetByIdAsync(stavka.UslugaId);
            if (usluga != null)
            {
                stavka.CijenaUTrenutkuNarudzbe = usluga.Cijena;
                ukupno += stavka.Kolicina * usluga.Cijena;
            }
        }
        return ukupno;
    }

    public async Task KreirajNarudzbu(Narudzba narudzba)
    {
        narudzba.DatumNarudzbe = DateTime.Now;
        narudzba.UkupnaCijena = await IzracunajUkupnuCijenu(narudzba.Stavke);
        await _narudzbaRepo.DodajNarudzbuAsync(narudzba);
    }
}
using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Suclja;

namespace IroningService.Servis.Implementacije;

public class NarudzbaServis : INarudzbaServis
{
    private readonly IUslugaRepozitorij _uslugaRepo;
    private readonly INarudzbaRepozitorij _narudzbaRepo;

    public NarudzbaServis(IUslugaRepozitorij uslugaRepo, INarudzbaRepozitorij narudzbaRepo)
    {
        _uslugaRepo = uslugaRepo;
        _narudzbaRepo = narudzbaRepo;
    }

    // Zamijenila sam GetAllAsync s DohvatiSveAsync
    // Ako se u tvom sučelju metoda zove drugačije, samo upiši to ime ovdje
    public async Task<IEnumerable<Narudzba>> DohvatiSveNarudzbeAsync()
    {
       return await _narudzbaRepo.GetAllAsync();
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
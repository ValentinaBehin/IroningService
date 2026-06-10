using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Suclja; 
using IroningService.Servis.Suclja;

namespace IroningService.Servis.Implementacije;

public class RecenzijaServis : IRecenzijaServis
{
    private readonly IRecenzijaRepozitorij _repo;

    public RecenzijaServis(IRecenzijaRepozitorij repo)
    {
        _repo = repo;
    }

    public async Task DodajRecenziju(Recenzija recenzija)
    {
        // Validacija
        if (recenzija.Ocjena < 1 || recenzija.Ocjena > 5)
        {
            throw new ArgumentException("Ocjena mora biti između 1 i 5.");
        }

        await _repo.DodajRecenzijuAsync(recenzija); 
    await _repo.SaveAsync();
    }

    public async Task<List<Recenzija>> DohvatiRecenzijeZaNarudzbuAsync(int narudzbaId)
    {
        return await _repo.DohvatiPoNarudzbiAsync(narudzbaId);
    }
}
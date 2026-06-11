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

    public async Task DodajRecenziju(RecenzijaModel recenzija)
    {
       await _repo.DodajRecenzijuAsync(recenzija);
        await _repo.SaveAsync();
    }

    public async Task<List<RecenzijaModel>> DohvatiRecenzijeZaNarudzbuAsync(int narudzbaId)
    {
        return await _repo.DohvatiPoNarudzbiAsync(narudzbaId);
    }
}
using IroningService.Domena.Entiteti;

namespace IroningService.Servis.Suclja;

public interface IRecenzijaServis
{
    Task DodajRecenziju(RecenzijaModel recenzija);
    Task<List<RecenzijaModel>> DohvatiRecenzijeZaNarudzbuAsync(int narudzbaId);
}
using IroningService.Domena.Entiteti;

namespace IroningService.Servis.Suclja;

public interface IRecenzijaServis
{
    Task DodajRecenziju(Recenzija recenzija);
    Task<List<Recenzija>> DohvatiRecenzijeZaNarudzbuAsync(int narudzbaId);
}
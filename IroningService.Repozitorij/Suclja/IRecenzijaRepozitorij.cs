using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Repozitoriji;

namespace IroningService.Repozitorij.Suclja;

public interface IRecenzijaRepozitorij
{
    Task DodajRecenzijuAsync(Recenzija recenzija);
    Task SaveAsync(); 
    Task<List<Recenzija>> DohvatiPoNarudzbiAsync(int narudzbaId);
}
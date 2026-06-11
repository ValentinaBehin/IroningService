using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Repozitoriji;

namespace IroningService.Repozitorij.Suclja;

public interface IRecenzijaRepozitorij
{
    Task DodajRecenzijuAsync(RecenzijaModel recenzija);
    Task SaveAsync(); 
    Task<List<RecenzijaModel>> DohvatiPoNarudzbiAsync(int narudzbaId);
}
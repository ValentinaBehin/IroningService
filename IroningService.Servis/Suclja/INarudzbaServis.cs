using IroningService.Domena.Entiteti;

namespace IroningService.Servis.Suclja;

public interface INarudzbaServis
{
    Task<IEnumerable<Narudzba>> DohvatiSveNarudzbeAsync();
    Task KreirajNarudzbu(Narudzba narudzba);
    Task<List<Narudzba>> DohvatiNarudzbePoEmailuAsync(string email); // Dodano
}
using IroningService.Domena.Entiteti;

namespace IroningService.Repozitorij.Suclja;

public interface INarudzbaRepozitorij
{
    Task DodajNarudzbuAsync(Narudzba narudzba);
    Task<IEnumerable<Narudzba>> GetAllAsync();
}
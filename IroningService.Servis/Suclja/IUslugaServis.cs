using IroningService.Domena.Entiteti;

namespace IroningService.Servis.Suclja;

public interface IUslugaServis
{
    Task<IEnumerable<UslugaPeglanja>> DohvatiSveUsluge();
}
using IroningService.Domena.Entiteti;

namespace IroningService.Repozitorij.Suclja;

public interface IUslugaRepozitorij
{
    Task<IEnumerable<UslugaPeglanja>> DohvatiSveUslugeAsync();
    Task<UslugaPeglanja?> GetByIdAsync(int id);
}
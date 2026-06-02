using IroningService.Domena.Entiteti;
using IroningService.Repozitorij.Suclja;
using IroningService.Servis.Suclja;

namespace IroningService.Servis.Implementacije;

public class UslugaServis : IUslugaServis
{
    private readonly IUslugaRepozitorij _repozitorij;

    public UslugaServis(IUslugaRepozitorij repozitorij)
    {
        _repozitorij = repozitorij;
    }

    public async Task<IEnumerable<UslugaPeglanja>> DohvatiSveUsluge()
    {
        return await _repozitorij.DohvatiSveUslugeAsync();
    }
}
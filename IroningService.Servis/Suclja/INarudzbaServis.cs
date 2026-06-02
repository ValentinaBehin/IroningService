using IroningService.Domena.Entiteti;

namespace IroningService.Servis.Suclja;

public interface INarudzbaServis
{
    Task<IEnumerable<Narudzba>> DohvatiSveNarudzbeAsync(); // Dodaj ovo
    Task<decimal> IzracunajUkupnuCijenu(List<StavkaNarudzbe> stavke);
    Task KreirajNarudzbu(Narudzba narudzba);
}
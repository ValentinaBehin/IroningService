using IroningService.Domena.Entiteti;

namespace IroningService.Domena.Entiteti;

public class Narudzba
{
    public int Id { get; set; }
    public string KlijentEmail { get; set; } = string.Empty;
    public DateTime DatumNarudzbe { get; set; }
    public DateTime TerminDostave { get; set; }
    public string? Adresa { get; set; } 
    public bool PotrebnaDostava { get; set; }
    public decimal UkupnaCijena { get; set; }
    public List<StavkaNarudzbe> Stavke { get; set; } = new();
}
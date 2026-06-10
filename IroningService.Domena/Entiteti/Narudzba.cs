using IroningService.Domena.Entiteti;
using System.ComponentModel.DataAnnotations;

namespace IroningService.Domena.Entiteti;

public class Narudzba
{
    [Key]
    public int NarudzbaId { get; set; }
    public int KorisnikId { get; set; }
    public string KlijentEmail { get; set; } = string.Empty;
    public DateTime DatumNarudzbe { get; set; }
    public DateTime TerminDostave { get; set; }
    public string? Adresa { get; set; } 
    public bool PotrebnaDostava { get; set; }
    public decimal UkupnaCijena { get; set; }
    public List<StavkaNarudzbe> Stavke { get; set; } = new();
    public string? RecenzijaKomentar { get; set; }
    public int? RecenzijaOcjena { get; set; }
}
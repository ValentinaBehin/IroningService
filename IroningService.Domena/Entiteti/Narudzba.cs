namespace IroningService.Domena.Entiteti;

public class Narudzba
{
    public Guid Id { get; set; }
    public DateTime DatumKreiranja { get; set; }
    public string Status { get; set; } = "Zaprimljeno";
    public ICollection<UslugaPeglanja> Usluge { get; set; } = new List<UslugaPeglanja>();
}
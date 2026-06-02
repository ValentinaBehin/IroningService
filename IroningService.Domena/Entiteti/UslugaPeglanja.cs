namespace IroningService.Domena.Entiteti;

public class UslugaPeglanja
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public decimal Cijena { get; set; }
    public MjernaJedinica Jedinica { get; set; }
}
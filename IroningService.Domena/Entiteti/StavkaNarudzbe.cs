namespace IroningService.Domena.Entiteti;

public class StavkaNarudzbe
{
    public int Id { get; set; }
    public int NarudzbaId { get; set; } 
    public int UslugaId { get; set; }
    public int Kolicina { get; set; }
    public decimal CijenaUTrenutkuNarudzbe { get; set; }
    public Narudzba Narudzba { get; set; } = null!;
}
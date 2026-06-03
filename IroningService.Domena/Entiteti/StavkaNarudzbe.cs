using System.Text.Json.Serialization; // Potrebno za [JsonIgnore]

namespace IroningService.Domena.Entiteti;

public class StavkaNarudzbe
{
    public int Id { get; set; }
    
    // Ovo je ključ koji povezuje stavku s narudžbom
    public int NarudzbaId { get; set; } 
    
    public int UslugaId { get; set; }
    public int Kolicina { get; set; }
    public decimal CijenaUTrenutkuNarudzbe { get; set; }

    // [JsonIgnore] sprječava da API pokušava serijalizirati cijeli objekt Narudzba
    // što uzrokuje onu "field is required" grešku.
    [JsonIgnore] 
    public Narudzba? Narudzba { get; set; }
    public UslugaPeglanja? Usluga { get; set; } 
}
public class Recenzija
{
    public int Id { get; set; }
    public int NarudzbaId { get; set; } // Poveznica na narudžbu
    public int Ocjena { get; set; }     // npr. 1-5
    public string Komentar { get; set; } = string.Empty;
    public DateTime DatumRecenzije { get; set; } = DateTime.Now;
}
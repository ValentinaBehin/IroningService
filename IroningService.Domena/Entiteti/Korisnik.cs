namespace IroningService.Domena.Entiteti;

public class Korisnik
{
    public int Id { get; set; }
    public string Ime { get; set; } = string.Empty;
    public string Prezime { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Lozinka { get; set; } = string.Empty; // Napomena: U produkciji uvijek hashiraj lozinke!
}
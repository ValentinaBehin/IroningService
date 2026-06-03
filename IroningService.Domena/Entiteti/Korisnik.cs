using System.ComponentModel.DataAnnotations;namespace IroningService.Domena.Entiteti;

public class Korisnik
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Ime je obavezno.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Ime mora imati između 2 i 50 znakova.")]
    public string Ime { get; set; }
    [Required(ErrorMessage = "Prezime je obavezno.")]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Prezime mora imati između 2 i 50 znakova.")]
    public string Prezime { get; set; }
    [Required(ErrorMessage = "Email je obavezan.")]
    [EmailAddress(ErrorMessage = "Format emaila nije ispravan.")]
    public string Email { get; set; }
    [Required]
    [MinLength(6, ErrorMessage = "Lozinka mora imati barem 6 znakova.")]
    public string Lozinka { get; set; }
}
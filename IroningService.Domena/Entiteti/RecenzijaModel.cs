using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IroningService.Domena.Entiteti;

public class RecenzijaModel
{
    [Key]
    public int Id { get; set; }
    public int NarudzbaId { get; set; } 
    
    [JsonPropertyName("komentar")]
    public string? Komentar { get; set; }
    
    [JsonPropertyName("ocjena")]
    public int? Ocjena { get; set; }
}
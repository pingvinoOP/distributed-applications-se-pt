using System.ComponentModel.DataAnnotations; // Add this

namespace PokemonApi.Models;

public class PokemonType
{
    public int Id { get; set; }
    [Required, MaxLength(30)]
    public string TypeName { get; set; }
    [MaxLength(30)]
    public string Weakness { get; set; }
    [MaxLength(30)]
    public string Resistance { get; set; }
    [MaxLength(7)]
    public string ColorCode { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }

    public List<Pokemon> Pokemon { get; set; } = new();
}
using System.ComponentModel.DataAnnotations; // Add this

namespace PokemonApi.DTOs;

public class PokemonDto
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Name { get; set; }
    [Required]
    public int HP { get; set; }
    public double Attack { get; set; }
    public long Defense { get; set; }
    public int Speed { get; set; }
    public DateTime CreatedDate { get; set; }
    public int PokemonTypeId { get; set; }
}
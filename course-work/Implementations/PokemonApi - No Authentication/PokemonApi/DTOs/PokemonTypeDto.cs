using System.ComponentModel.DataAnnotations;

namespace PokemonApi.DTOs;

public class PokemonTypeDto
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
}
using System.ComponentModel.DataAnnotations;

namespace PokemonApi.DTOs;

public class LoginDto
{
    [Required, MaxLength(50)]
    public string Username { get; set; }
    [Required, MaxLength(100)]
    public string Password { get; set; }
}
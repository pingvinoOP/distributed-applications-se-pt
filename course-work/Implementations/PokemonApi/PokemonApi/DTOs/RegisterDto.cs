using System.ComponentModel.DataAnnotations;

namespace PokemonApi.DTOs;

public class RegisterDto
{
    [Required, MaxLength(50)]
    public string Username { get; set; }
    [Required, MaxLength(100)]
    public string Password { get; set; }
    [MaxLength(50)]
    public string Email { get; set; }
}
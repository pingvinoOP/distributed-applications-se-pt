using System.ComponentModel.DataAnnotations; // Add this

namespace PokemonApi.Models;

public class User
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string Username { get; set; }
    [Required, MaxLength(100)]
    public string PasswordHash { get; set; }
    [MaxLength(50)]
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; }
    public bool IsActive { get; set; }
}
﻿using System.ComponentModel.DataAnnotations; // Add this

namespace PokemonApi.Models;

public class Move
{
    public int Id { get; set; }
    [Required, MaxLength(50)]
    public string MoveName { get; set; }
    public int Power { get; set; }
    public double Accuracy { get; set; }
    public int PP { get; set; }
    [MaxLength(200)]
    public string Description { get; set; }
    public DateTime LastUpdated { get; set; }

    public List<PokemonMove> PokemonMoves { get; set; } = new();
}
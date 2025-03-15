using Microsoft.EntityFrameworkCore;
using PokemonApi.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace PokemonApi.Data;

public class PokemonDbContext : DbContext
{
    public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options) { }

    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<PokemonType> PokemonTypes { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<PokemonMove> PokemonMoves { get; set; }
    public DbSet<User> Users { get; set; } 

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonMove>()
            .HasKey(pm => new { pm.PokemonId, pm.MoveId });

        modelBuilder.Entity<PokemonMove>()
            .HasOne(pm => pm.Pokemon)
            .WithMany(p => p.PokemonMoves)
            .HasForeignKey(pm => pm.PokemonId);

        modelBuilder.Entity<PokemonMove>()
            .HasOne(pm => pm.Move)
            .WithMany(m => m.PokemonMoves)
            .HasForeignKey(pm => pm.MoveId);

        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = 1,
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password123"), // Hashed password
                Email = "admin@pokemonapi.com",
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            }
        );
    }
}
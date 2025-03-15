using Microsoft.EntityFrameworkCore;
using PokemonApi.Models;

namespace PokemonApi.Data;

public class PokemonDbContext : DbContext
{
    public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options) { }

    public DbSet<Pokemon> Pokemon { get; set; }
    public DbSet<PokemonType> PokemonTypes { get; set; }
    public DbSet<Move> Moves { get; set; }
    public DbSet<PokemonMove> PokemonMoves { get; set; }

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

        // Seed a default Pokémon Type
        modelBuilder.Entity<PokemonType>().HasData(
            new PokemonType
            {
                Id = 1,
                TypeName = "Electric",
                Weakness = "Ground",
                Resistance = "Flying",
                ColorCode = "#FFFF00",
                CreatedDate = DateTime.UtcNow,
                IsActive = true
            }
        );
    }
}
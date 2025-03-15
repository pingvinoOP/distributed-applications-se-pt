namespace PokemonApi.Models;

public class PokemonMove
{
    public int PokemonId { get; set; }
    public Pokemon Pokemon { get; set; }

    public int MoveId { get; set; }
    public Move Move { get; set; }
}
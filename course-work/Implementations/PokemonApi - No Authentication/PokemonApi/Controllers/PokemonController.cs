using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.DTOs;
using PokemonApi.Models;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonController : ControllerBase
{
    private readonly PokemonDbContext _context;

    public PokemonController(PokemonDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetPokemon(int page = 1, int pageSize = 10)
    {
        var pokemon = await _context.Pokemon
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new PokemonDto { Id = p.Id, Name = p.Name, HP = p.HP, Attack = p.Attack, Defense = p.Defense, Speed = p.Speed, CreatedDate = p.CreatedDate, PokemonTypeId = p.PokemonTypeId })
            .ToListAsync();
        return Ok(pokemon);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchPokemonByName(string name)
    {
        var pokemon = await _context.Pokemon
            .Where(p => p.Name.Contains(name))
            .Select(p => new PokemonDto { Id = p.Id, Name = p.Name, HP = p.HP, Attack = p.Attack, Defense = p.Defense, Speed = p.Speed, CreatedDate = p.CreatedDate, PokemonTypeId = p.PokemonTypeId })
            .ToListAsync();
        return Ok(pokemon);
    }

    [HttpPost]
    public async Task<IActionResult> CreatePokemon([FromBody] PokemonDto dto)
    {
        var pokemon = new Pokemon { Name = dto.Name, HP = dto.HP, Attack = dto.Attack, Defense = dto.Defense, Speed = dto.Speed, CreatedDate = dto.CreatedDate, PokemonTypeId = dto.PokemonTypeId };
        _context.Pokemon.Add(pokemon);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPokemon), new { id = pokemon.Id }, pokemon);
    }
}
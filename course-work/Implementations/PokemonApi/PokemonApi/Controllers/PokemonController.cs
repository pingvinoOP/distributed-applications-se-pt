using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.DTOs;
using PokemonApi.Models;

namespace PokemonApi.Controllers;

[Authorize]
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
        var pokemon = new Pokemon
        {
            Name = dto.Name,
            HP = dto.HP,
            Attack = dto.Attack,
            Defense = dto.Defense,
            Speed = dto.Speed,
            CreatedDate = dto.CreatedDate,
            PokemonTypeId = dto.PokemonTypeId
        };

        _context.Pokemon.Add(pokemon);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetPokemon), new { id = pokemon.Id }, pokemon);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePokemon(int id, [FromBody] PokemonDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID mismatch.");
        }

        var existingPokemon = await _context.Pokemon.FindAsync(id);
        if (existingPokemon == null)
        {
            return NotFound("Pokemon not found.");
        }

        existingPokemon.Name = dto.Name;
        existingPokemon.HP = dto.HP;
        existingPokemon.Attack = dto.Attack;
        existingPokemon.Defense = dto.Defense;
        existingPokemon.Speed = dto.Speed;
        existingPokemon.PokemonTypeId = dto.PokemonTypeId;

        _context.Pokemon.Update(existingPokemon);
        await _context.SaveChangesAsync();

        return NoContent(); 
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePokemon(int id)
    {
        var pokemon = await _context.Pokemon.FindAsync(id);
        if (pokemon == null)
        {
            return NotFound("Pokemon not found.");
        }

        _context.Pokemon.Remove(pokemon);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

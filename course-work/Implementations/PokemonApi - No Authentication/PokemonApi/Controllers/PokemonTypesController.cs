
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonApi.Data;
using PokemonApi.DTOs;
using PokemonApi.Models;

namespace PokemonApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PokemonTypesController : ControllerBase
{
    private readonly PokemonDbContext _context;

    public PokemonTypesController(PokemonDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetTypes(int page = 1, int pageSize = 10)
    {
        var types = await _context.PokemonTypes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(t => new PokemonTypeDto { Id = t.Id, TypeName = t.TypeName, Weakness = t.Weakness, Resistance = t.Resistance, ColorCode = t.ColorCode, CreatedDate = t.CreatedDate, IsActive = t.IsActive })
            .ToListAsync();
        return Ok(types);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchTypesByName(string typeName)
    {
        var types = await _context.PokemonTypes
            .Where(t => t.TypeName.Contains(typeName))
            .Select(t => new PokemonTypeDto { Id = t.Id, TypeName = t.TypeName, Weakness = t.Weakness, Resistance = t.Resistance, ColorCode = t.ColorCode, CreatedDate = t.CreatedDate, IsActive = t.IsActive })
            .ToListAsync();
        return Ok(types);
    }

    [HttpPost]
    public async Task<IActionResult> CreateType([FromBody] PokemonTypeDto dto)
    {
        var type = new PokemonType { TypeName = dto.TypeName, Weakness = dto.Weakness, Resistance = dto.Resistance, ColorCode = dto.ColorCode, CreatedDate = dto.CreatedDate, IsActive = dto.IsActive };
        _context.PokemonTypes.Add(type);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetTypes), new { id = type.Id }, type);
    }
}
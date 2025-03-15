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
public class MovesController : ControllerBase
{
    private readonly PokemonDbContext _context;

    public MovesController(PokemonDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetMoves(int page = 1, int pageSize = 10)
    {
        var moves = await _context.Moves
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new MoveDto { Id = m.Id, MoveName = m.MoveName, Power = m.Power, Accuracy = m.Accuracy, PP = m.PP, Description = m.Description, LastUpdated = m.LastUpdated })
            .ToListAsync();
        return Ok(moves);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchMovesByName(string moveName)
    {
        var moves = await _context.Moves
            .Where(m => m.MoveName.Contains(moveName))
            .Select(m => new MoveDto { Id = m.Id, MoveName = m.MoveName, Power = m.Power, Accuracy = m.Accuracy, PP = m.PP, Description = m.Description, LastUpdated = m.LastUpdated })
            .ToListAsync();
        return Ok(moves);
    }

    [HttpPost]
    public async Task<IActionResult> CreateMove([FromBody] MoveDto dto)
    {
        var move = new Move { MoveName = dto.MoveName, Power = dto.Power, Accuracy = dto.Accuracy, PP = dto.PP, Description = dto.Description, LastUpdated = dto.LastUpdated };
        _context.Moves.Add(move);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetMoves), new { id = move.Id }, move);
    }
}
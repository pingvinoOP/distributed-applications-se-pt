using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Pokedex.Data;
using Pokedex.Models;

namespace Pokedex.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly PokedexDbContext _context;

        public PokemonsController(PokedexDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
           
            return View(await _context.Pokemons.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.PokemonTypes)
                    .ThenInclude(pt => pt.Type)
                .Include(p => p.PokemonMoves)
                    .ThenInclude(pm => pm.Moves)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        public IActionResult Create()
        {
            ViewBag.Types = new SelectList(_context.Types, "Id", "Name");
            ViewBag.Moves = new SelectList(_context.Moves, "Id", "Name");
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,ImgUrl,Description")] Pokemon pokemon, int[] selectedTypeIds, int[] selectedMoveIds)
        {
            if (ModelState.IsValid)
            {
                if (selectedTypeIds != null)
                {
                    foreach (var typeId in selectedTypeIds)
                    {
                        pokemon.PokemonTypes.Add(new PokemonType { TypeId = typeId });
                    }
                }

                if (selectedMoveIds != null)
                {
                    foreach (var moveId in selectedMoveIds)
                    {
                        pokemon.PokemonMoves.Add(new PokemonMove { MoveId = moveId });
                    }
                }

                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Types = new SelectList(_context.Types, "Id", "Name");
            ViewBag.Moves = new SelectList(_context.Moves, "Id", "Name");
            return View(pokemon);
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.PokemonTypes)
                .Include(p => p.PokemonMoves)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pokemon == null)
            {
                return NotFound();
            }

            ViewBag.Types = new SelectList(await _context.Types.ToListAsync(), "Id", "Name", pokemon.PokemonTypes.Select(pt => pt.TypeId));
            ViewBag.Moves = new SelectList(await _context.Moves.ToListAsync(), "Id", "Name", pokemon.PokemonMoves.Select(pm => pm.MoveId));

            return View(pokemon);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,ImgUrl,Description")] Pokemon pokemon, int[] selectedTypeIds, int[] selectedMoveIds)
        {
            if (id != pokemon.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var existingPokemon = await _context.Pokemons
                    .Include(p => p.PokemonTypes)
                    .Include(p => p.PokemonMoves)
                    .FirstOrDefaultAsync(p => p.Id == id);

                if (existingPokemon == null)
                {
                    return NotFound();
                }

                existingPokemon.Name = pokemon.Name;
                existingPokemon.ImgUrl = pokemon.ImgUrl;
                existingPokemon.Description = pokemon.Description;

                _context.PokemonTypes.RemoveRange(existingPokemon.PokemonTypes);
                _context.PokemonMoves.RemoveRange(existingPokemon.PokemonMoves);

                if (selectedTypeIds != null)
                {
                    foreach (var typeId in selectedTypeIds)
                    {
                        existingPokemon.PokemonTypes.Add(new PokemonType { PokemonId = id, TypeId = typeId });
                    }
                }

                if (selectedMoveIds != null)
                {
                    foreach (var moveId in selectedMoveIds)
                    {
                        existingPokemon.PokemonMoves.Add(new PokemonMove { PokemonId = id, MoveId = moveId });
                    }
                }

                try
                {
                    _context.Update(existingPokemon);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    Console.WriteLine($"Error updating Pokemon: {ex.Message}");
                    ModelState.AddModelError("", "An error occurred while updating the database.");
                }
            }

            ViewBag.Types = new SelectList(await _context.Types.ToListAsync(), "Id", "Name", selectedTypeIds);
            ViewBag.Moves = new SelectList(await _context.Moves.ToListAsync(), "Id", "Name", selectedMoveIds);

            return View(pokemon);
        }



        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(int id)
        {
            return _context.Pokemons.Any(e => e.Id == id);
        }
    }
}

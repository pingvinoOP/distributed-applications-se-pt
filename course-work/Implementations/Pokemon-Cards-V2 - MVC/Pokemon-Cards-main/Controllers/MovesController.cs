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
    public class MovesController : Controller
    {
        private readonly PokedexDbContext _context;

        public MovesController(PokedexDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Moves.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moves = await _context.Moves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moves == null)
            {
                return NotFound();
            }

            return View(moves);
        }

        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Moves moves)
        {
            if (ModelState.IsValid)
            {
                _context.Add(moves);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(moves);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moves = await _context.Moves.FindAsync(id);
            if (moves == null)
            {
                return NotFound();
            }
            return View(moves);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Moves moves)
        {
            if (id != moves.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moves);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovesExists(moves.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(moves);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moves = await _context.Moves
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moves == null)
            {
                return NotFound();
            }

            return View(moves);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moves = await _context.Moves.FindAsync(id);
            if (moves != null)
            {
                _context.Moves.Remove(moves);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovesExists(int id)
        {
            return _context.Moves.Any(e => e.Id == id);
        }
    }
}

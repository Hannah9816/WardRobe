using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WardRobe.Data;
using WardRobe.Models;

namespace WardRobe.Views.Backpacks
{
    public class BackpacksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BackpacksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Backpacks
        public async Task<IActionResult> Index()
        {
            return View(await _context.Backpack.ToListAsync());
        }

        // GET: Backpacks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backpack = await _context.Backpack
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backpack == null)
            {
                return NotFound();
            }

            return View(backpack);
        }

        // GET: Backpacks/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Backpacks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TripName,Date,WardrobeId,UserId")] Backpack backpack)
        {
            if (ModelState.IsValid)
            {
                _context.Add(backpack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(backpack);
        }

        // GET: Backpacks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backpack = await _context.Backpack.FindAsync(id);
            if (backpack == null)
            {
                return NotFound();
            }
            return View(backpack);
        }

        // POST: Backpacks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TripName,Date,WardrobeId,UserId")] Backpack backpack)
        {
            if (id != backpack.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(backpack);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BackpackExists(backpack.Id))
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
            return View(backpack);
        }

        // GET: Backpacks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var backpack = await _context.Backpack
                .FirstOrDefaultAsync(m => m.Id == id);
            if (backpack == null)
            {
                return NotFound();
            }

            return View(backpack);
        }

        // POST: Backpacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var backpack = await _context.Backpack.FindAsync(id);
            _context.Backpack.Remove(backpack);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BackpackExists(int id)
        {
            return _context.Backpack.Any(e => e.Id == id);
        }
    }
}

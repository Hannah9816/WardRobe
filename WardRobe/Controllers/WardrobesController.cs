using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WardRobe.Data;
using WardRobe.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WardRobe.Views.Wardrobes
{
    public class WardrobesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public WardrobesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Wardrobes
        public async Task<IActionResult> Index(string searchString)
        {
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);

            var userid = _userManager.GetUserId(HttpContext.User);

            var wardrobe = from m in _context.Wardrobe select m;

            if (!String.IsNullOrEmpty(userid))
            {
                wardrobe = wardrobe.Where(m => m.UserId.Contains(userid));
            }

            return View(await wardrobe.AsNoTracking().ToListAsync());
        }

        // GET: Wardrobes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wardrobe = await _context.Wardrobe
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wardrobe == null)
            {
                return NotFound();
            }

            return View(wardrobe);
        }

        // GET: Wardrobes/Create
        public IActionResult Create()
        {
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            return View();
        }
       
        // POST: Wardrobes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Category,Name,Brand,PurchaseDate,Price,WornTimes,ImageUrl,UserId")] Wardrobe wardrobe)
        {
            ViewBag.userid = _userManager.GetUserId(HttpContext.User);
            if (ModelState.IsValid)
            {
                    /*if (Image != null && Image.Length > 0)
                    {

                        var file = Image;
                        var uploads = Path.Combine("wwwroot\\images");

                        if (file.Length > 0)
                        {
                            var fileName = ContentDispositionHeaderValue.Parse
                                (file.ContentDisposition).FileName.Trim('"');

                            System.Console.WriteLine(fileName);
                            using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                            {
                                await file.CopyToAsync(fileStream);
                                wardrobe.ImageUrl = file.FileName;
                            }
                        }
                    }*/


                    _context.Add(wardrobe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                }
              /*  else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                }*/
            return View(wardrobe);
        }

        // GET: Wardrobes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wardrobe = await _context.Wardrobe.FindAsync(id);
            if (wardrobe == null)
            {
                return NotFound();
            }
            return View(wardrobe);
        }

        // POST: Wardrobes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Category,Name,Brand,PurchaseDate,Price,WornTimes,ImageUrl,UserId")] Wardrobe wardrobe)
        {
            if (id != wardrobe.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wardrobe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WardrobeExists(wardrobe.ID))
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
            return View(wardrobe);
        }

        // GET: Wardrobes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wardrobe = await _context.Wardrobe
                .FirstOrDefaultAsync(m => m.ID == id);
            if (wardrobe == null)
            {
                return NotFound();
            }

            return View(wardrobe);
        }

        // POST: Wardrobes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wardrobe = await _context.Wardrobe.FindAsync(id);
            _context.Wardrobe.Remove(wardrobe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WardrobeExists(int id)
        {
            return _context.Wardrobe.Any(e => e.ID == id);
        }
    }
}

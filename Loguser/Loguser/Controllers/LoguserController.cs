using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Loguser.Data;
using Loguser.Models;
using Microsoft.AspNetCore.Authorization;

namespace Loguser.Controllers
{
    [Authorize]
    public class LoguserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoguserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Loguser
        public async Task<IActionResult> Index()
        {
              return _context.Loguser != null ? 
                          View(await _context.Loguser.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Loguser'  is null.");
        }

        // GET: Loguser/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Loguser == null)
            {
                return NotFound();
            }

            var loguserEntity = await _context.Loguser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loguserEntity == null)
            {
                return NotFound();
            }

            return View(loguserEntity);
        }

        // GET: Loguser/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Loguser/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Password,FirstName,LastName,Email")] LoguserEntity loguserEntity)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loguserEntity);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loguserEntity);
        }

        // GET: Loguser/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Loguser == null)
            {
                return NotFound();
            }

            var loguserEntity = await _context.Loguser.FindAsync(id);
            if (loguserEntity == null)
            {
                return NotFound();
            }
            return View(loguserEntity);
        }

        // POST: Loguser/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserName,Password,FirstName,LastName,Email")] LoguserEntity loguserEntity)
        {
            if (id != loguserEntity.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loguserEntity);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoguserEntityExists(loguserEntity.Id))
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
            return View(loguserEntity);
        }

        // GET: Loguser/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Loguser == null)
            {
                return NotFound();
            }

            var loguserEntity = await _context.Loguser
                .FirstOrDefaultAsync(m => m.Id == id);
            if (loguserEntity == null)
            {
                return NotFound();
            }

            return View(loguserEntity);
        }

        // POST: Loguser/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Loguser == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Loguser'  is null.");
            }
            var loguserEntity = await _context.Loguser.FindAsync(id);
            if (loguserEntity != null)
            {
                _context.Loguser.Remove(loguserEntity);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoguserEntityExists(int id)
        {
          return (_context.Loguser?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

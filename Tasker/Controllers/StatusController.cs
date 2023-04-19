using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tasker.Data;
using Tasker.Models;

namespace Tasker.Controllers
{
    public class StatusController : Controller
    {
        private readonly TaskerContext _db;

        public StatusController(TaskerContext context)
        {
            _db = context;
        }

        // GET: Status
        public async Task<IActionResult> Index()
        {
              return _db.Statuses != null ? 
                          View(await _db.Statuses.ToListAsync()) :
                          Problem("Entity set 'TaskerContext.Status'  is null.");
        }

        // GET: Status/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Statuses == null)
            {
                return NotFound();
            }

            var status = await _db.Statuses
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // GET: Status/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Status/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StatusId,StatusName")] Status status)
        {
            if (ModelState.IsValid)
            {
                _db.Add(status);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(status);
        }

        // GET: Status/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Statuses == null)
            {
                return NotFound();
            }

            var status = await _db.Statuses.FindAsync(id);
            if (status == null)
            {
                return NotFound();
            }
            return View(status);
        }

        // POST: Status/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StatusId,StatusName")] Status status)
        {
            if (id != status.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(status);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StatusExists(status.StatusId))
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
            return View(status);
        }

        // GET: Status/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Statuses == null)
            {
                return NotFound();
            }

            var status = await _db.Statuses
                .FirstOrDefaultAsync(m => m.StatusId == id);
            if (status == null)
            {
                return NotFound();
            }

            return View(status);
        }

        // POST: Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Statuses == null)
            {
                return Problem("Entity set 'TaskerContext.Status'  is null.");
            }
            var status = await _db.Statuses.FindAsync(id);
            if (status != null)
            {
                _db.Statuses.Remove(status);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StatusExists(int id)
        {
          return (_db.Statuses?.Any(e => e.StatusId == id)).GetValueOrDefault();
        }
    }
}

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
    public class TaskksController : Controller
    {
        private readonly TaskerContext _context;

        public TaskksController(TaskerContext context)
        {
            _context = context;
        }

        // GET: Taskks
        public async Task<IActionResult> Index()
        {
            var taskerContext = _context.Task.Include(t => t.Status);
            return View(await taskerContext.ToListAsync());
        }

        // GET: Taskks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Task == null)
            {
                return NotFound();
            }

            var taskk = await _context.Task
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (taskk == null)
            {
                return NotFound();
            }

            return View(taskk);
        }

        // GET: Taskks/Create
        public IActionResult Create()
        {
            ViewData["StatusId"] = new SelectList(_context.Set<Status>(), "StatusId", "StatusId");
            return View();
        }

        // POST: Taskks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,ParentTaskId,TaskName,TaskDesc,DoerUserId,TaskMasterUserId,StatusId,DateCreate,DeadLine,TaskCost")] Taskk taskk)
        {
            if (ModelState.IsValid)
            {
                _context.Add(taskk);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StatusId"] = new SelectList(_context.Set<Status>(), "StatusId", "StatusId", taskk.StatusId);
            return View(taskk);
        }

        // GET: Taskks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Task == null)
            {
                return NotFound();
            }

            var taskk = await _context.Task.FindAsync(id);
            if (taskk == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_context.Set<Status>(), "StatusId", "StatusId", taskk.StatusId);
            return View(taskk);
        }

        // POST: Taskks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TaskId,ParentTaskId,TaskName,TaskDesc,DoerUserId,TaskMasterUserId,StatusId,DateCreate,DeadLine,TaskCost")] Taskk taskk)
        {
            if (id != taskk.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(taskk);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskkExists(taskk.TaskId))
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
            ViewData["StatusId"] = new SelectList(_context.Set<Status>(), "StatusId", "StatusId", taskk.StatusId);
            return View(taskk);
        }

        // GET: Taskks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Task == null)
            {
                return NotFound();
            }

            var taskk = await _context.Task
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (taskk == null)
            {
                return NotFound();
            }

            return View(taskk);
        }

        // POST: Taskks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Task == null)
            {
                return Problem("Entity set 'TaskerContext.Task'  is null.");
            }
            var taskk = await _context.Task.FindAsync(id);
            if (taskk != null)
            {
                _context.Task.Remove(taskk);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskkExists(int id)
        {
          return (_context.Task?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}

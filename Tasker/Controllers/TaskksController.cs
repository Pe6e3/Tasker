using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Tasker.Data;
using Tasker.Models;

namespace Tasker.Controllers
{
    public class TaskksController : Controller
    {
        private readonly TaskerContext _db;

        public TaskksController(TaskerContext context)
        {
            _db = context;
        }

        // GET: Taskks
        public async Task<IActionResult> Index()
        {
            var taskerContext = _db.Tasks
                .Include(t => t.UserDoer)
                .Include(t => t.UserMaster)
                .Include(t => t.Status);
            return View(await taskerContext.ToListAsync());
        }

        // GET: Taskks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Tasks == null)
            {
                return NotFound();
            }

            var taskk = await _db.Tasks
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
            ViewBag.Users = _db.Users.ToList();
            ViewBag.Statuses = _db.Statuses.ToList();
            ViewBag.Tasks = _db.Tasks.ToList();
            return View();
        }

        // POST: Taskks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,ParentTaskId,TaskName,TaskDesc,DoerUserId,TaskMasterUserId,StatusId,DateCreate,DeadLine,TaskCost")] Taskk taskk)
        {
            ViewBag.UserMaster = new SelectList(_db.Users, "UserId", "UserName", taskk.UserMaster);
            ViewBag.UserDoer = new SelectList(_db.Users, "UserId", "UserName", taskk.UserDoer);
            if (ModelState.IsValid)
            {
                _db.Add(taskk);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(taskk);
        }

        // GET: Taskks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Tasks == null)
            {
                return NotFound();
            }

            var taskk = await _db.Tasks.FindAsync(id);
            if (taskk == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_db.Set<Status>(), "StatusId", "StatusId", taskk.StatusId);
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
                    _db.Update(taskk);
                    await _db.SaveChangesAsync();
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
            ViewData["StatusId"] = new SelectList(_db.Set<Status>(), "StatusId", "StatusId", taskk.StatusId);
            return View(taskk);
        }

        // GET: Taskks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Tasks == null)
            {
                return NotFound();
            }

            var taskk = await _db.Tasks
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
            if (_db.Tasks == null)
            {
                return Problem("Entity set 'TaskerContext.Task'  is null.");
            }
            var taskk = await _db.Tasks.FindAsync(id);
            if (taskk != null)
            {
                _db.Tasks.Remove(taskk);
            }
            
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskkExists(int id)
        {
          return (_db.Tasks?.Any(e => e.TaskId == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tasker.Data;
using Tasker.Models;

namespace Tasker.Controllers
{
    public class MissionsController : Controller
    {
        private readonly TaskerContext _db;

        public MissionsController(TaskerContext context)
        {
            _db = context;
        }



        // GET: Missions
        public async Task<IActionResult> Index()
        {
            var taskerContext = _db.Missions
                .Include(t => t.UserDoer)
                .Include(t => t.UserMaster)
                .Include(t => t.Status);
            return View(await taskerContext.ToListAsync());
        }

        // GET: Missions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.Missions == null)
            {
                return NotFound();
            }

            var Mission = await _db.Missions
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.MissionId == id);
            if (Mission == null)
            {
                return NotFound();
            }

            return View(Mission);
        }

        // GET: Missions/Create
        public IActionResult Create()
        {
            //ViewBag.Users = _db.Users.ToList();
            //ViewBag.Statuses = _db.Statuses.ToList();
            //ViewBag.Missions = _db.Missions.ToList();

            ViewBag.Statuses = new SelectList(_db.Statuses, "StatusId", "StatusName");
            ViewBag.Missions = new SelectList(_db.Missions, "MissionId", "MissionName");
            ViewBag.UserMaster = new SelectList(_db.Users, "UserId", "UserName");
            ViewBag.UserDoer = new SelectList(_db.Users, "UserId", "UserName");

            return View();
        }

        // POST: Missions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MissionId,ParentMissionId,MissionName,MissionDesc,DoerUserId,MissionMasterUserId,StatusId,DateCreate,DeadLine,MissionCost")] Mission Mission)
        {
            ViewBag.Statuses = new SelectList(_db.Statuses, "StatusId", "StatusName");
            ViewBag.Missions = new SelectList(_db.Missions, "MissionId", "MissionName");
            ViewBag.UserMaster = new SelectList(_db.Users, "UserId", "UserName");
            ViewBag.UserDoer = new SelectList(_db.Users, "UserId", "UserName");

                _db.Add(Mission);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            //if (ModelState.IsValid)
            //{
            //    _db.Add(Mission);
            //    await _db.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            //return View(Mission);
        }

        // GET: Missions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.Missions == null)
            {
                return NotFound();
            }

            var Mission = await _db.Missions.FindAsync(id);
            if (Mission == null)
            {
                return NotFound();
            }
            ViewData["StatusId"] = new SelectList(_db.Set<Status>(), "StatusId", "StatusId", Mission.StatusId);
            return View(Mission);
        }

        // POST: Missions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MissionId,ParentMissionId,MissionName,MissionDesc,DoerUserId,MissionMasterUserId,StatusId,DateCreate,DeadLine,MissionCost")] Mission Mission)
        {
            if (id != Mission.MissionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(Mission);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MissionExists(Mission.MissionId))
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
            ViewData["StatusId"] = new SelectList(_db.Set<Status>(), "StatusId", "StatusId", Mission.StatusId);
            return View(Mission);
        }

        // GET: Missions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.Missions == null)
            {
                return NotFound();
            }

            var Mission = await _db.Missions
                .Include(t => t.Status)
                .FirstOrDefaultAsync(m => m.MissionId == id);
            if (Mission == null)
            {
                return NotFound();
            }

            return View(Mission);
        }

        // POST: Missions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.Missions == null)
            {
                return Problem("Entity set 'TaskerContext.Task'  is null.");
            }
            var Mission = await _db.Missions.FindAsync(id);
            if (Mission != null)
            {
                _db.Missions.Remove(Mission);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MissionExists(int id)
        {
            return (_db.Missions?.Any(e => e.MissionId == id)).GetValueOrDefault();
        }
    }
}

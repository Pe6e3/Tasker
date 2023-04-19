using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tasker.Data;
using Tasker.Models;

namespace Tasker.Controllers
{
    public class UsersController : Controller
    {
        private readonly TaskerContext _db;

        public UsersController(TaskerContext context)
        {
            _db = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
           var userContext =  _db.User
                .Include(p=>p.Role);

            return View(await userContext.ToListAsync());
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _db.User == null)
            {
                return NotFound();
            }

            var user = await _db.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,UserName,Gender,AvatarPath,Login,Password,RoleId")] User user)
        {
            if (ModelState.IsValid)
            {
                if (user.AvatarPath == null && user.Gender == "М") user.AvatarPath = "/image/AvatarM.jpg";
                if (user.AvatarPath == null && user.Gender == "Ж") user.AvatarPath = "/image/AvatarF.jpg";

                Role role = _db.Roles.Where(r => r.RoleId == user.RoleId).FirstOrDefault();
                _db.Add(user);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _db.User == null)
            {
                return NotFound();
            }

            var user = await _db.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,UserName,Gender,AvatarPath,Login,Password,RoleId")] User user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(user);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.UserId))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _db.User == null)
            {
                return NotFound();
            }

            var user = await _db.User
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_db.User == null)
            {
                return Problem("Entity set 'TaskerContext.User'  is null.");
            }
            var user = await _db.User.FindAsync(id);
            if (user != null)
            {
                _db.User.Remove(user);
            }

            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return (_db.User?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}

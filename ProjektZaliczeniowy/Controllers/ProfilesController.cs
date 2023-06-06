using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProjektZaliczeniowy.Data;
using ProjektZaliczeniowy.Models;

namespace ProjektZaliczeniowy.Controllers
{
    public class ProfilesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public ProfilesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        // GET: Profiles
        public async Task<IActionResult> Index(string profileuser)
        {
            ViewData["ProfileUser"] = profileuser;
            ViewData["CurrentUser"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            //var applicationDbContext = _context.Profile.Include(p => p.User);
            //await applicationDbContext.ToListAsync()
            return View();
        }

        // GET: Profiles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            /*
            if (id == null || _context.Profile == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);
            */
            return RedirectToAction("Index", "Home");
        }

        // GET: Profiles/Create
        public IActionResult Create()
        {
            ViewData["CurrentUser"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        // POST: Profiles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserID,Name,Surname,Description,BirthDate,Image")] Profile profile, IFormFile Image)
        {
            if (Image == null || Image.Length == 0)
            {
                return Content("File not selected");
            }

            var path = Path.Combine(_environment.WebRootPath, "images/awatar", Image.FileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
                stream.Close();
            }

            profile.Image = Image.FileName;

            if (ModelState.IsValid)
            {
                _context.Add(profile);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", profile.UserID);
            return View(profile);
        }

        // GET: Profiles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Profile == null)
            {
                return NotFound();
            }
            var profile = await _context.Profile.FindAsync(id);
            if (profile == null)
            {
                return NotFound();
            }

            if (profile.UserID != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            ViewData["CurrentUser"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(profile);
        }

        // POST: Profiles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserID,Name,Surname,Description,BirthDate,Image")] Profile profile, IFormFile Image)
        {
            if (id != profile.Id)
            {
                return NotFound();
            }

            if (Image == null || Image.Length == 0)
            {
                return Content("File not selected");
            }

            var path = Path.Combine(_environment.WebRootPath, "images/awatar", Image.FileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
                stream.Close();
            }

            profile.Image = Image.FileName;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(profile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProfileExists(profile.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", profile.UserID);
            return RedirectToAction("Index", "Home");
        }

        // GET: Profiles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            /*
            if (id == null || _context.Profile == null)
            {
                return NotFound();
            }

            var profile = await _context.Profile
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);*/
            return RedirectToAction("Index", "Home");
        }

        // POST: Profiles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            /*
            if (_context.Profile == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Profile'  is null.");
            }
            var profile = await _context.Profile.FindAsync(id);
            if (profile != null)
            {
                _context.Profile.Remove(profile);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            */
            return RedirectToAction("Index", "Home");
        }

        private bool ProfileExists(int id)
        {
          return _context.Profile.Any(e => e.Id == id);
        }
    }
}

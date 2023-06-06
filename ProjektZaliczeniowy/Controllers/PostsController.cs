using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using ProjektZaliczeniowy.Data;
using ProjektZaliczeniowy.Models;

namespace ProjektZaliczeniowy.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;
        public PostsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _environment = env;
        }

        // GET: Posts
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Post.Include(p => p.User);
            //return View(await applicationDbContext.ToListAsync());
            return RedirectToAction("Index", "Home");
        }

        // GET: Posts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }
            ViewData["CurrentUser"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["Dump"] = post.UserID;
            var lista = _context.Comment.Where(c => c.CommentedPost == post.Id);
            ViewData["ComList"] = await lista.ToListAsync();
            return View(post);
        }

        // GET: Posts/Create
        public IActionResult Create()
        {
            ViewData["CurrentUser"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserID,Title,Description,Image")] Post post, IFormFile Image)
        {

            if (Image == null || Image.Length == 0)
            {
                return Content("File not selected");
            }

            var path = Path.Combine(_environment.WebRootPath, "images/", Image.FileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
                stream.Close();
            }

            post.Image = Image.FileName;

            if (ModelState.IsValid)
            {

                _context.Add(post);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", post.UserID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            if (post.UserID != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", post.UserID);
            ViewData["CurrentUser"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,UserID,Title,Description,Image")] Post post, IFormFile Image)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            if (Image == null || Image.Length == 0)
            {
                return Content("File not selected");
            }

            var path = Path.Combine(_environment.WebRootPath, "images/", Image.FileName);
            using (FileStream stream = new FileStream(path, FileMode.Create))
            {
                await Image.CopyToAsync(stream);
                stream.Close();
            }

            post.Image = Image.FileName;



            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(post);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostExists(post.Id))
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
            ViewData["UserID"] = new SelectList(_context.Users, "Id", "Id", post.UserID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Post == null)
            {
                return NotFound();
            }

            var post = await _context.Post
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (post == null)
            {
                return NotFound();
            }

            if (post.UserID != User.FindFirstValue(ClaimTypes.NameIdentifier))
            {
                return Forbid();
            }

            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Post == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Post'  is null.");
            }
            var post = await _context.Post.FindAsync(id);
            if (post != null)
            {
                _context.Post.Remove(post);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }

        private bool PostExists(int id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}

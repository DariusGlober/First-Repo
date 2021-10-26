using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using APIGenerator.Data;
using APIGenerator.Models;
using APIGenerator.ActionFilters;
using Microsoft.AspNetCore.Authorization;

namespace APIGenerator.Controllers
{
    [Authorize]
    [ApiController]
    //[Route("Memes")]
    public class MemeController : Controller
    {
        private readonly APIGeneratorContext _context;

        public MemeController(APIGeneratorContext context)
        {
            _context = context;
        }

        // GET: Memes
        [Route("Memes/Index")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Memes.ToListAsync());
        }

        // GET: Memes/Details/5
        [HttpGet("Details/{id}")]        
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memes = await _context.Memes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memes == null)
            {
                return NotFound();
            }

            return View(memes);
        }

        // GET: Memes/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Memes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Memes/Create")]
        public async Task<IActionResult> Create([Bind("Id,Name,Image")] Memes memes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(memes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(memes);
        }

        // GET: Memes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memes = await _context.Memes.FindAsync(id);
            if (memes == null)
            {
                return NotFound();
            }
            return View(memes);
        }

        // POST: Memes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Image")] Memes memes)
        {
            if (id != memes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(memes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemesExists(memes.Id))
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
            return View(memes);
        }

        // GET: Memes/Delete/5
        [HttpGet]
        [ServiceFilter(typeof(ValidationFilter))]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var memes = await _context.Memes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (memes == null)
            {
                return NotFound();
            }

            return View(memes);
        }

        // POST: Memes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var memes = await _context.Memes.FindAsync(id);
            _context.Memes.Remove(memes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MemesExists(int id)
        {
            return _context.Memes.Any(e => e.Id == id);
        }
    }
}

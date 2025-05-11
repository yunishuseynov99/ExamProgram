using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamProgram.Models;
using ExamProgram.Data;

namespace ExamProgram.Controllers
{
    public class SubjectController : Controller
    {
        private readonly AppDbContext _context;

        public SubjectController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var subjects = await _context.Subjects.ToListAsync();
            return View(subjects);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Subject subject)
        {
            if (await _context.Subjects.AnyAsync(s => s.SubjectCode == subject.SubjectCode)) 
            {
                ModelState.AddModelError("", "this subject code already exists");
                return View(subject);
            }

            if (ModelState.IsValid)
            {
                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subject);
        }

        public async Task<IActionResult> Edit(string id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            return View(subject);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, Subject subject)
        {
            if (id != subject.SubjectCode)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subject);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Subjects.Any(e => e.SubjectCode == subject.SubjectCode))
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
            return View(subject);
        }

        public async Task<IActionResult> Delete(string id)
        {
            var subject = await _context.Subjects
                .FirstOrDefaultAsync(m => m.SubjectCode == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var subject = await _context.Subjects.FindAsync(id);
            _context.Subjects.Remove(subject);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

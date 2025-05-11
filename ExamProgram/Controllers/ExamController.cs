using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamProgram.Models;
using ExamProgram.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExamProgram.DTOs;

namespace ExamProgram.Controllers
{
    public class ExamController : Controller
    {
        private readonly AppDbContext _context;

        public ExamController(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<IActionResult> Index()
        {
            var exams = await _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.Student)
                .ToListAsync();
            return View(exams);
        }

        public IActionResult Create()
        {
            ViewData["Subjects"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectName");
            ViewData["Students"] = new SelectList(_context.Students, "StudentNumber", "FirstName");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ExamDto examDto)
        {
            if (ModelState.IsValid)
            {
                var exam = new Exam
                {
                    ExamDate = examDto.ExamDate,
                    StudentNumber = examDto.StudentNumber,
                    SubjectCode = examDto.SubjectCode,
                    Grade = examDto.Grade,
                };
                _context.Exams.Add(exam);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Subjects"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectName");
            ViewData["Students"] = new SelectList(_context.Students, "StudentNumber", "FirstName");
            return View(examDto);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            if (exam == null)
            {
                return NotFound();
            }

            ViewData["Subjects"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectName", exam.SubjectCode);
            ViewData["Students"] = new SelectList(_context.Students, "StudentNumber", "FirstName", exam.StudentNumber);
            return View(exam);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Exam exam)
        {
            if (id != exam.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exam);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Exams.Any(e => e.Id == exam.Id))
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

            ViewData["Subjects"] = new SelectList(_context.Subjects, "SubjectCode", "SubjectName", exam.SubjectCode);
            ViewData["Students"] = new SelectList(_context.Students, "StudentNumber", "FirstName", exam.StudentNumber);
            return View(exam);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var exam = await _context.Exams
                .Include(e => e.Subject)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (exam == null)
            {
                return NotFound();
            }

            return View(exam);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var exam = await _context.Exams.FindAsync(id);
            _context.Exams.Remove(exam);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}

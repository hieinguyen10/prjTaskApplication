using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Task_Application.Data;
using Task_Application.Models;
using Task = Task_Application.Models.Task;

namespace Task_Application.Controllers
{
    public class TasksController : Controller
    {
        private readonly taskmanagementContext _context;

        public TasksController(taskmanagementContext context)
        {
            _context = context;
        }

        // GET: Tasks
        public async Task<IActionResult> Index()
        {
            var taskmanagementContext = _context.Tasks.Include(t => t.AssignedNavigation);
            return View(await taskmanagementContext.ToListAsync());
        }

        // GET: Tasks/MyTask/EmployeeId

        public async Task<IActionResult> MyTask(int id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.Where(m => m.Assigned==id).ToListAsync();
            if (task == null)
            {
                return NotFound();
            }
           
            return View(task);
        }

        // GET: Tasks/Create
        public IActionResult Create()
        {
            ViewData["Assigned"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId");
            return View();
        }

        // POST: Tasks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TaskId,TaskDescription,IsDone,Deadline,Assigned")] Task task)
        {
            if (ModelState.IsValid)
            {
                _context.Add(task);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Assigned"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", task.Assigned);
            return View(task);
        }

        // GET: Tasks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewData["Assigned"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", task.Assigned);
            return View(task);
        }

        // POST: Tasks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("TaskId,TaskDescription,IsDone,Deadline,Assigned")] Task task)
        {
            if (id != task.TaskId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(task);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TaskExists(task.TaskId))
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
            ViewData["Assigned"] = new SelectList(_context.Employees, "EmployeeId", "EmployeeId", task.Assigned);
            return View(task);
        }

        // GET: Tasks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Tasks == null)
            {
                return NotFound();
            }

            var task = await _context.Tasks
                .Include(t => t.AssignedNavigation)
                .FirstOrDefaultAsync(m => m.TaskId == id);
            if (task == null)
            {
                return NotFound();
            }

            return View(task);
        }

        // POST: Tasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Tasks == null)
            {
                return Problem("Entity set 'taskmanagementContext.Tasks'  is null.");
            }
            var task = await _context.Tasks.FindAsync(id);
            if (task != null)
            {
                _context.Tasks.Remove(task);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TaskExists(string id)
        {
          return _context.Tasks.Any(e => e.TaskId == id);
        }
    }
}

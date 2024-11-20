using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApp.Contexts;
using TodoApp.Models;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        public async Task<IActionResult> Show()
        {
            using (TodoEfContext context = new TodoEfContext())
            {
                return View(await context.Todos.ToListAsync());
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Todo todo)
        {
            using (TodoEfContext context = new TodoEfContext())
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Show));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (!id.HasValue) return BadRequest();
            Todo? data;
            using (TodoEfContext context = new TodoEfContext())
            {
                data = await context.Todos.FindAsync(id);
            }

            if (data is null) return NotFound();


            return View(data);
        }
    }
}

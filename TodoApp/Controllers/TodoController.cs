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
                todo.IsDone = false;
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Show));
        }

        [HttpGet]
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

        [HttpPost]
        public async Task<IActionResult> Update(int? id, Todo newTodo)
        {
            Todo? data;
            if (!id.HasValue) return BadRequest();
            using (TodoEfContext context = new TodoEfContext())
            {
                data = await context.Todos.FindAsync(id.Value);
                if (data is null) return NotFound();
                data.Title = newTodo.Title;
                data.Description = newTodo.Description;
                data.Deadline = newTodo.Deadline;
                await context.SaveChangesAsync();
            }


            return RedirectToAction(nameof(Show));
        }


        [HttpPost]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    Todo? data;
        //    if (!id.HasValue) return BadRequest();
        //    using (TodoEfContext context = new TodoEfContext())
        //    {
        //        data = await context.Todos.FindAsync(id.Value);
        //        if (data is null) return NotFound();

        //        if (await context.Todos.AnyAsync(x => x.Id == id))
        //        {
        //            context.Todos.Remove(data);
        //            await context.SaveChangesAsync();

        //        }
        //    }


        //    return RedirectToAction(nameof(Show));
        //}

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            using (TodoEfContext context = new TodoEfContext())
            {
                var data = await context.Todos.FindAsync(id);
                if (data == null)
                {
                    return NotFound();
                }

                context.Todos.Remove(data);
                await context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Show));
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
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

        public async Task<IActionResult> Finish(int? id)
        {
            if (!id.HasValue) return BadRequest();
            Todo? data;
            using (TodoEfContext context = new TodoEfContext())
            {
                data = await context.Todos.FindAsync(id);
                data.IsDone = true;
                await context.SaveChangesAsync();
            }

            if (data is null) return NotFound();

            return RedirectToAction(nameof(Show));
        }
    }
}

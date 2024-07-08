using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDocker.Models;

namespace ToDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly TodoContext _context;

        public TodoItemsController(TodoContext context)
        {
            _context = context;

            if (_context.todos.Count() == 0)
            {
                _context.todos.Add(new TodoItem { title = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            return await _context.todos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            var todoItem = await _context.todos.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem;
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem item)
        {
            _context.todos.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodoItem), new { id = item.id }, item);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem item)
        {
            if (id != item.id)
            {
                return BadRequest();
            }

            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodoItem(long id)
        {
            var todoItem = await _context.todos.FindAsync(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            _context.todos.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}

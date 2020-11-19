using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TodoApi.Models;
using TodoApi.Services.bbdd;

namespace TodoApi.Services.negocio.Implementacion
{
    public class TodoItemsService : ITodoItemsService
    {
        private readonly TodoContext _context;

        public TodoItemsService(TodoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TodoItem>> GetTodoItems()
        {
            return await _context.TodoItems
                .ToListAsync();
        }

        public async Task<TodoItem> GetTodoItem(long id)
        {
            //Find vs Where + First OR Where + Single OR First/Single: https://stackoverflow.com/questions/9335015/find-vs-where-firstordefault

            TodoItem response = await _context.TodoItems
                .SingleOrDefaultAsync(item => item.Id == id); //devuelve the entity found, or null.

            return response;
        }

        public async Task PostTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Add(todoItem);

            await _context.SaveChangesAsync();
        }

        public async Task PutTodoItem(TodoItem todoItem)
        {
            _context.Entry(todoItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<TodoItem> DeleteTodoItem(TodoItem todoItem)
        {
            _context.TodoItems.Remove(todoItem);
            await _context.SaveChangesAsync();

            return todoItem;
        }
    }
}
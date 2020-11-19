using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<TodoItemDto>> GetTodoItems()
        {
            IEnumerable<TodoItem> items = await _context.TodoItems
                .ToListAsync();

            return items
                .Select(item => ItemToDto(item))
                .ToList();
        }

        public async Task<TodoItemDto> GetTodoItem(long id)
        {
            //Find vs Where + First OR Where + Single OR First/Single: https://stackoverflow.com/questions/9335015/find-vs-where-firstordefault
            TodoItem item = await GetItem(id); //devuelve the entity found, or null.

            if(item == null)
            {
                return await Task.FromResult<TodoItemDto>(null);
            }

            return ItemToDto(item);
        }

        public async Task<TodoItemDto> CreateTodoItem(TodoItemDto itemDto)
        {
            TodoItem item = DtoToItem(itemDto);

            _context.TodoItems.Add(item);

            await _context.SaveChangesAsync();

            return ItemToDto(item);
        }

        public async Task UpdateTodoItem(TodoItemDto itemDto)
        {
            try
            {
                TodoItem item = await GetItem(itemDto.Id);
                if(item == null)
                {
                    throw new Exception($"Item con {itemDto.Id} not found.");
                }

                item.Name = itemDto.Name;
                item.IsComplete = itemDto.IsComplete;

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<TodoItemDto> DeleteTodoItem(TodoItemDto itemDto)
        {
            TodoItem item = await GetItem(itemDto.Id);

            _context.TodoItems.Remove(item);
            await _context.SaveChangesAsync();

            return itemDto;
        }

        private async Task<TodoItem> GetItem(long id)
        {
            //Find vs Where + First OR Where + Single OR First/Single: https://stackoverflow.com/questions/9335015/find-vs-where-firstordefault

            TodoItem item = await _context.TodoItems
                .SingleOrDefaultAsync(item => item.Id == id); //devuelve the entity found, or null.

            return item;
        }

        private static TodoItemDto ItemToDto(TodoItem item)
        {
            return new TodoItemDto
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }

        private static TodoItem DtoToItem(TodoItemDto item)
        {
            return new TodoItem
            {
                Id = item.Id,
                Name = item.Name,
                IsComplete = item.IsComplete
            };
        }
    }
}
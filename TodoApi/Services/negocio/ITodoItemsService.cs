using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services.negocio
{
    public interface ITodoItemsService
    {
        Task<IEnumerable<TodoItemDto>> GetTodoItems();
        Task<TodoItemDto> GetTodoItem(long id);
        Task<TodoItemDto> CreateTodoItem(TodoItemDto todoItemDto);
        Task UpdateTodoItem(TodoItemDto todoItemDto);
        Task<TodoItemDto> DeleteTodoItem(TodoItemDto todoItemDto);
    }
}
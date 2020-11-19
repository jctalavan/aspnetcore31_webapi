using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApi.Models;

namespace TodoApi.Services.negocio
{
    public interface ITodoItemsService
    {
        Task<IEnumerable<TodoItem>> GetTodoItems();
        Task<TodoItem> GetTodoItem(long id);
        Task PostTodoItem(TodoItem todoItem);
        Task PutTodoItem(TodoItem todoItem);
    }
}
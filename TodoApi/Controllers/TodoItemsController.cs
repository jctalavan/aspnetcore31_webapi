using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Services.negocio;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoItemsController : ControllerBase
    {
        private readonly ITodoItemsService _todoItemsService;

        public TodoItemsController(ITodoItemsService todoItemsService)
        {
            _todoItemsService = todoItemsService;
        }

        // GET: api/TodoItems
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TodoItem>>> GetTodoItems()
        {
            IEnumerable<TodoItem> todoItems = await _todoItemsService.GetTodoItems();

            return Ok(todoItems.ToList()); // no se admite como resultado una interfaz
        }

        // GET: api/TodoItems/5
        // Para tener más información acerca del enlace de modelos, visitar las siguientes URL:
        // https://docs.microsoft.com/es-es/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1
        // https://docs.microsoft.com/es-es/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItem>> GetTodoItem(long id)
        {
            TodoItem todoItem = await _todoItemsService.GetTodoItem(id);

            if (todoItem == null)
            {
                return NotFound();
            }

            return todoItem; // ASP.NET Core serializa automáticamente el objeto a JSON y escribe el JSON en el cuerpo del mensaje de la respuesta HTTP 200
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(long id, TodoItem todoItem)
        {
            if (id != todoItem.Id)
            {
                return BadRequest();
            }

            try
            {
                await _todoItemsService.PutTodoItem(todoItem);
            }
            catch (DbUpdateConcurrencyException)
            {
                TodoItem todoItemGet = await _todoItemsService.GetTodoItem(todoItem.Id);
                if (todoItemGet == null)
                {
                    return NotFound();
                }
            }

            return NoContent();
        }

        // POST: api/TodoItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<TodoItem>> PostTodoItem(TodoItem todoItem)
        {
            await _todoItemsService.PostTodoItem(todoItem);

            //Una petición POST donde normalmente se crea un recurso en el servidor normalmente provee al cliente que ha
            //hecho el POST de una URL que puede usar para hacer un GET y obtener el recurso recien creado.
            //Para ello, lo que se suele hacer es añadir un Header en la respuesta. Pues bien, al devolver una respuesta
            //de tipo 'CreatedAtAction', el ObjectResult ya hace esto por nosotros y genera la URL con la información
            //que pasamos a CreatedAtAction.

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItem.Id }, todoItem);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItem>> DeleteTodoItem(long id)
        {
            var todoItem = await _todoItemsService.GetTodoItem(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            TodoItem todoItemDeleted = await _todoItemsService.DeleteTodoItem(todoItem);

            return todoItemDeleted;
        }
    }
}

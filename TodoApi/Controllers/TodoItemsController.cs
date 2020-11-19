using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<IEnumerable<TodoItemDto>>> GetTodoItems()
        {
            IEnumerable<TodoItemDto> items = await _todoItemsService.GetTodoItems();

            return items.ToList(); // no se admite como resultado una interfaz
        }

        // GET: api/TodoItems/5
        // Para tener más información acerca del enlace de modelos, visitar las siguientes URL:
        // https://docs.microsoft.com/es-es/aspnet/core/mvc/controllers/routing?view=aspnetcore-3.1
        // https://docs.microsoft.com/es-es/aspnet/core/mvc/models/model-binding?view=aspnetcore-3.1
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TodoItemDto>> GetTodoItem(long id)
        {
            TodoItemDto item = await _todoItemsService.GetTodoItem(id);

            if (item == null)
            {
                return NotFound();
            }

            return item; // ASP.NET Core serializa automáticamente el objeto a JSON y escribe el JSON en el cuerpo del mensaje de la respuesta HTTP 200
        }

        // PUT: api/TodoItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTodoItem(long id, TodoItemDto todoItemDto)
        {
            if (id != todoItemDto.Id)
            {
                return BadRequest();
            }

            try
            {
                await _todoItemsService.UpdateTodoItem(todoItemDto);
            }
            catch (Exception)
            {
                TodoItemDto itemGet = await _todoItemsService.GetTodoItem(todoItemDto.Id);
                if (itemGet == null)
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
        public async Task<ActionResult<TodoItemDto>> CreateTodoItem(TodoItemDto todoItemDto)
        {
            TodoItemDto todoItemDtoCreated = await _todoItemsService.CreateTodoItem(todoItemDto);

            //Una petición POST donde normalmente se crea un recurso en el servidor normalmente provee al cliente que ha
            //hecho el POST de una URL que puede usar para hacer un GET y obtener el recurso recien creado.
            //Para ello, lo que se suele hacer es añadir un Header en la respuesta. Pues bien, al devolver una respuesta
            //de tipo 'CreatedAtAction', el ObjectResult ya hace esto por nosotros y genera la URL con la información
            //que pasamos a CreatedAtAction.

            return CreatedAtAction(nameof(GetTodoItem), new { id = todoItemDtoCreated.Id }, todoItemDtoCreated);
        }

        // DELETE: api/TodoItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TodoItemDto>> DeleteTodoItem(long id)
        {
            var item = await _todoItemsService.GetTodoItem(id);
            if (item == null)
            {
                return NotFound();
            }

            TodoItemDto itemDeleted = await _todoItemsService.DeleteTodoItem(item);

            return itemDeleted;
        }
    }
}

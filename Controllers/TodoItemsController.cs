using Microsoft.AspNetCore.Mvc;
using ToDoList.Api.Module;
using ToDoList.Api.services;


namespace ToDoList.Api.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TodoItemsController:ControllerBase
    {
        private readonly ILogger<TodoItemsController> _logger;
        private ITodoItemService _service;

        public TodoItemsController(ILogger<TodoItemsController> logger,ITodoItemService inMemoryToDoListItemsService)
        {
            _logger = logger;
            _service = inMemoryToDoListItemsService;
        }

        [HttpGet()]
        public async Task<ActionResult<List<TodoItem>>> GetAll()
        {
            var result=await _service.GetAllAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TodoItem>> GetById(string id)
        {
            var result=await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(result);
            }
        }

        [HttpPost]
        public async Task<ActionResult<TodoItem>> CreateItem(TodoItemCreateRequest createRequest)
        {
            TodoItem item = new TodoItem();
            item.Id = Guid.NewGuid().ToString();
            item.isFavorite=createRequest.isFavorite;
            item.IsDone=createRequest.IsDone;
            item.Description=createRequest.Description;
            var result=await _service.CreateItemAsync(item);
            return Created("",result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TodoItem>> UpdateItemById(string id,TodoItem requestBody)
        {
            var existItem= await _service.GetByIdAsync(id);
            bool isCreate = false;
            if (existItem == null)
            {
                isCreate = true;
                await  _service.CreateItemAsync(requestBody);

            }
            else
            {
                requestBody.CreatedTime= DateTime.Now;
                _service.UpdateItemByIdAsync(id, requestBody);
            }
            return isCreate ? Created("", requestBody) : Ok(requestBody);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItemById(string id)
        {
            await _service.DeleteItemAsync(id);
            return NoContent();
        }

    }
}

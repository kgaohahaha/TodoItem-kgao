using ToDoList.Api.Module;

namespace ToDoList.Api.services
{
    public interface ITodoItemService
    {
        Task<TodoItem> CreateItemAsync(TodoItem request);
        Task DeleteItemAsync(string id);
        Task<List<TodoItem>> GetAllAsync();
        Task<TodoItem?> GetByIdAsync(string id);
        Task UpdateItemByIdAsync(string id, TodoItem requestBody);
    }
}
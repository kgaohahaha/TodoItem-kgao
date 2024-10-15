using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ToDoList.Api.Module;

namespace ToDoList.Api.services
{
    public class ToDoListItemsService : ITodoItemService
    {
        private static readonly List<TodoItem> _todoItems = new List<TodoItem>();

        private readonly IMongoCollection<TodoItemsDB> _ToDoItemsCollection;

        public ToDoListItemsService(IMongoClient mongoClient,IOptions<TodoItemDBSetting> ToDoItemStoreDatabaseSettings)
        {
            var mongoDatabase = mongoClient.GetDatabase(
                ToDoItemStoreDatabaseSettings.Value.DatabaseName);

            _ToDoItemsCollection = mongoDatabase.GetCollection<TodoItemsDB>(
                ToDoItemStoreDatabaseSettings.Value.CollectionName);
        }

        public async Task<List<TodoItem>> GetAllAsync()
        {
            var res= await _ToDoItemsCollection.Find(_=>true).ToListAsync();
            var result= new List<TodoItem>();
            foreach (var item in res)
            {
                result.Add(item.convert(item));
            }
            return result;
        }

        public async Task<TodoItem?> GetByIdAsync(string id)
        {
            var result = await _ToDoItemsCollection.Find(x=>x.Id==id).FirstOrDefaultAsync();
            return result.convert(result);
        }

        public async Task<TodoItem> CreateItemAsync(TodoItem request)
        {
            await _ToDoItemsCollection.InsertOneAsync(request.convertDB(request));
            return request;
        }

        public async Task UpdateItemByIdAsync(string id, TodoItem requestBody)
        {
            await _ToDoItemsCollection.ReplaceOneAsync(x=>x.Id==id,requestBody.convertDB(requestBody));
        }

        public async Task DeleteItemAsync(string id)
        {
            var result = await _ToDoItemsCollection.DeleteOneAsync(x => x.Id == id);
        }
    }
}

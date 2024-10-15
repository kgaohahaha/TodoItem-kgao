using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ToDoList.Api.Module
{
    public record TodoItemsDB
    {
        [BsonId]
        public required string Id { get; set; }
        public required string Description { get; set; }
        public required bool Done { get; set; }
        public required bool Favorite { get; set; }

        [BsonRepresentation(BsonType.String)]
        public required DateTimeOffset CreatedTime { get; init; }

        public TodoItem convert(TodoItemsDB item)
        {
            TodoItem todoItem = new TodoItem()
            {
                Id = item.Id,
                Description = item.Description,
                CreatedTime = item.CreatedTime,
                IsDone = item.Done,
                isFavorite = item.Favorite
            };
            return todoItem;
        }
    }
}

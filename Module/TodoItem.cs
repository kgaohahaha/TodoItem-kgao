namespace ToDoList.Api.Module
{
    public record TodoItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; }
        public DateTimeOffset CreatedTime { get; set; }= DateTimeOffset.Now;
        public bool isFavorite { get; set; }

        public TodoItemsDB convertDB(TodoItem item)
        {
            TodoItemsDB todoItemsDB = new TodoItemsDB() { 
                Id = item.Id,
                Description = item.Description,
                CreatedTime = item.CreatedTime,
                Done = item.IsDone,
                Favorite=item.isFavorite
            };
            return todoItemsDB;
        }
    }
}

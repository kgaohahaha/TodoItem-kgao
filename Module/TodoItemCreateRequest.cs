namespace ToDoList.Api.Module
{
    public class TodoItemCreateRequest
    {
        public string Description { get; set; } = string.Empty;
        public bool IsDone { get; set; }
        public bool isFavorite { get; set; }
    }
}

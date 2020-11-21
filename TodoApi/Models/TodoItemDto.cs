namespace TodoApi.Models
{
    public class TodoItemDto
    {
        /// <summary>
        /// Identificador del elemento TodoItem.
        /// </summary>
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
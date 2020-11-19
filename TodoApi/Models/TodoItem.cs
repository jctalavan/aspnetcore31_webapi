namespace TodoApi.Models
{
    public class TodoItem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsComplete { get; set; }
        public string Secret { get; set; } //ejemplo de campo que creamos para comprobar que no sale en las respuestas de nuestro controller TodoItems
    }
}
namespace WebApplication3.Models
{
    public class ToDoItems
    {
        public int Id { get; set; }
        public int CatId { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? CompletedDate { get; set; }
    }
}

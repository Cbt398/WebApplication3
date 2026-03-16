namespace WebApplication3.Models
{
    public class ToDoViewModel
    {
        public List<ToDoItems> ToDo { get; set; }
        public List<Category> Categories { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}

using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace WebApplication3.Models
{
    public class ToDoManager
    {
        private string _connectionString;


        public ToDoManager(string connectionString)
        {
            _connectionString = connectionString;
        }


        public List<ToDoItems> GetItems(bool isCompleted)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();

            if (isCompleted)
            {
                cmd.CommandText = @"SELECT td.*, c.Category AS Category    
                                    FROM ToDoItems td
                                    JOIN Categories c ON c.Id = td.CatId
                                    WHERE td.CompletedDate IS NOT NULL";
            }
            else
            {
                cmd.CommandText = @"SELECT td.*, c.Category AS Category    
                                    FROM ToDoItems td
                                    JOIN Categories c ON c.Id = td.CatId
                                    WHERE td.CompletedDate IS NULL";
            }

            connection.Open();

            var reader = cmd.ExecuteReader();
            var items = new List<ToDoItems>();
            while (reader.Read())
            {
                items.Add(new ToDoItems
                {
                    Id = (int)reader["Id"],
                    CatId = (int)reader["CatId"],
                    Title = (string)reader["Title"],
                    DueDate = (DateTime)reader["DueDate"],
                    CompletedDate = reader.GetOrNull<DateTime?>("CompletedDate"),
                    Category = (string)reader["Category"]
                });


            }
            connection.Close();
            return items;

        }


        public List<Category> GetCategories()
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM Categories";
            connection.Open();

            var reader = cmd.ExecuteReader();
            var categories = new List<Category>();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    Id = (int)reader["Id"],
                    Name = (string)reader["Category"]
                });
            }
            connection.Close();
            return categories;

        }

        public void AddCategory(Category category)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO Categories (Category) VALUES (@name)";
            cmd.Parameters.AddWithValue("@name", category.Name);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public void EditCategory(Category category)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE Categories SET Category = @name WHERE ID = @Id";
            cmd.Parameters.AddWithValue("@name", category.Name);
            cmd.Parameters.AddWithValue("@Id", category.Id);
            connection.Open();
            cmd.ExecuteNonQuery();
        }

        public Category GetCategoryById(int catId)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = @"SELECT * FROM Categories WHERE Id= @Id";
            cmd.Parameters.AddWithValue("@Id", catId);

            connection.Open();

            var reader = cmd.ExecuteReader();
            reader.Read();

            var category = new Category
            {
                Id = (int)reader["Id"],
                Name = (string)reader["Category"]
            };

            connection.Close();
            return category;


        }

        public void AddItem(ToDoItems toDo)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "INSERT INTO ToDoItems (Title, DueDate, CatId) VALUES (@title, @dueDate, @catId)";
            cmd.Parameters.AddWithValue("@title", toDo.Title);
            cmd.Parameters.AddWithValue("@dueDate", toDo.DueDate);
            cmd.Parameters.AddWithValue("@catId", toDo.CatId);
            connection.Open();
            cmd.ExecuteNonQuery();
        }


        public void CompleteToDo(int toDoId)
        {
            var connection = new SqlConnection(_connectionString);
            var cmd = connection.CreateCommand();
            cmd.CommandText = "UPDATE ToDoItems SET CompletedDate = @completedDate WHERE Id = @id";
            cmd.Parameters.AddWithValue("@completedDate", DateTime.Now);
            cmd.Parameters.AddWithValue("@id", toDoId);
            connection.Open();
            cmd.ExecuteNonQuery();
        }
    }
}

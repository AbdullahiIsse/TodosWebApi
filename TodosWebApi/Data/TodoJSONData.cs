using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data
{
    public class TodoJsonData : ITodoData
    {
        private string todoFile = "todos.json";
        private IList<Todo> todos;

        public TodoJsonData()
        {
            if (!File.Exists(todoFile))
            {
                Seed();
                WriteTodossToFile();
            }
            else
            {
                string content = File.ReadAllText(todoFile);
                todos = JsonSerializer.Deserialize<List<Todo>>(content);
            }
        }

        private void Seed()
        {
            Todo[] ts =
            {
                new Todo {UserId = 1, TodoId = 1, Title = "Do dishes", IsCompleted = false},
                new Todo {UserId = 1, TodoId = 2, Title = "Walk the dog", IsCompleted = false},
                new Todo {UserId = 2, TodoId = 3, Title = "Do DNP homework", IsCompleted = true},
                new Todo {UserId = 3, TodoId = 4, Title = "Eat breakfast", IsCompleted = false},
                new Todo {UserId = 4, TodoId = 5, Title = "Mow lawn", IsCompleted = true},
            };
            todos = ts.ToList();
        }


        public IList<Todo> GetTodos()
        {
            List<Todo> tmp = new List<Todo>(todos);
            return tmp;
        }

        public Todo AddTodo(Todo todo)
        {
            int max = todos.Max(todo => todo.TodoId);
            todo.TodoId = (++max);
            todos.Add(todo);
            WriteTodossToFile();

            return todo;
        }

        public void RemoveTodo(int todoId)
        {
            Todo toRemove = todos.First(t => t.TodoId == todoId);
            todos.Remove(toRemove);
            WriteTodossToFile();
        }

        public Todo Update(Todo todo)
        {
            Todo toUpdate = todos.First(t => t.TodoId == todo.TodoId);
            toUpdate.IsCompleted = todo.IsCompleted;
            toUpdate.Title = todo.Title;
            WriteTodossToFile();
            return todo;
        }

        public Todo Get(int id)
        {
            return todos.FirstOrDefault(t => t.TodoId == id);
        }

        private void WriteTodossToFile()
        {
            string todosAsJson = JsonSerializer.Serialize(todos);
            File.WriteAllText(todoFile, todosAsJson);
        }
    }
}
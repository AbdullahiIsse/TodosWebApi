using System.Collections.Generic;
using AdvancedTodo.Models;

namespace AdvancedTodo.Data
{
    public interface ITodoData
    {


        IList<Todo> GetTodos();
        Todo AddTodo(Todo todo);
        void RemoveTodo(int todoId);

        Todo Update(Todo todo);

        Todo Get(int id);






    }
}
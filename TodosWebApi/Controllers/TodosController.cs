using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Xml;
using System.Threading.Tasks;
using AdvancedTodo.Data;
using AdvancedTodo.Models;
using Microsoft.AspNetCore.Mvc;

namespace TodosWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TodosController : ControllerBase
    {
        private ITodoData iTodoData;

        public TodosController(ITodoData iTodoData)
        {
            this.iTodoData = iTodoData;
        }


        [HttpGet]
        public async Task<ActionResult<IList<Todo>>> GetTodos([FromQuery] bool? isCompleted, [FromQuery] int? userId)
        {
            try
            {
                IEnumerable<Todo> todos = iTodoData.GetTodos();
                if (isCompleted != null && userId != null)
                {
                    todos = todos.Where(todo => todo.IsCompleted.Equals(isCompleted) && todo.UserId.Equals(userId));
                }

                return Ok(todos);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult<Todo>> AddTodo([FromBody] Todo todo)
        {
            try
            {
                Todo todoAdded = iTodoData.AddTodo(todo);
                return Created($"/{todoAdded.TodoId}", todoAdded);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> DeleteTodo([FromRoute] int id)
        {
            try
            {
                iTodoData.RemoveTodo(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }


        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<Todo>> UpdateTodo([FromBody] Todo todo)
        {
            try
            {
                Todo update = iTodoData.Update(todo);
                return Ok(update);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
              return  StatusCode(500, e.Message);
            }
        }
    }
}
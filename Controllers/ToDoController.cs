using System.Collections.Generic;
using IIS.Models;
using IIS.Service;
using Microsoft.AspNetCore.Mvc;

namespace IIS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private ToDoService _service = new ToDoService();

        /// <summary>
        /// Get all ToDos
        /// </summary>
        /// <returns>List of ToDos</returns>
        [HttpGet]
        public IEnumerable<ToDoModel> GetAll()
        {
            return _service.GetAllTodos();
        }

        
        /// <summary>
        /// Add new ToDo
        /// </summary>
        /// <param name="todo">Object of new todo</param>
        /// <returns>Bool</returns>
        [HttpPost]
        public ToDoModel Post([FromBody] ToDoModel todo)
        {
            return _service.AddTodo(todo);
        }

        
        /// <summary>
        /// Update todo
        /// </summary>
        /// <param name="toDoId">Id of todo</param>
        /// <param name="todo">Object of todo as it should be</param>
        [HttpPut("{toDoId}")]
        public bool Put(int toDoId, [FromBody] ToDoModel todo)
        {
            return _service.UpdateTodo(todo, toDoId);
        }

    }
}

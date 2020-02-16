using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Models;

namespace api.Data.Repositories
{
    /// <summary>
    /// Used for dependency injection for ToDoRepository.
    /// </summary>
    public interface IToDoRepository
    {
        public IEnumerable<ToDo> GetToDos(int[] toDoIds);
    }
}

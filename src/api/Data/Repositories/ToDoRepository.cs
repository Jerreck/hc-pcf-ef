using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data.Models;
using api.Data.DbContexts;

namespace api.Data.Repositories
{
    /// <summary>
    /// Data access methods for ToDos.
    /// </summary>
    public class ToDoRepository : IToDoRepository
    {
        private readonly ToDoDbContext _context;

        public ToDoRepository(ToDoDbContext context)
        {
            this._context = context;
        }

        public IEnumerable<ToDo> GetToDos(int[] toDoIds)
        {
            if (toDoIds.Length == 0)
            {
                return this._context.ToDos.ToList();
            }

            return this._context.ToDos
                .Where(b => toDoIds.Contains(b.Id))
                .ToList();
        }
    }
}

using System.Collections.Generic;
using api.Data.Models;
using api.Data.Repositories;
using HotChocolate;
using HotChocolate.Types;

namespace api.Graph.Queries
{
    [ExtendObjectType(Name = "Query")]
    public class ToDoQuery
    {
        public IEnumerable<ToDo> GetToDos([Service]IToDoRepository repository, int[] toDoIds) => repository.GetToDos(toDoIds);
    }
}

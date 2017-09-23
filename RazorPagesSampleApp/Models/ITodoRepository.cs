using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSampleApp.Models
{
    public interface ITodoRepository
    {
        Task<IEnumerable<Todo>> GetAll();
        Task Add(Todo todo);
        Task Remove(Guid id);
        Task<Todo> Find(Guid id);
        Task Update(Todo todo);
    }
}

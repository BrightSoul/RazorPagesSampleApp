using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSampleApp.Models;

namespace RazorPagesSampleApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ITodoRepository todoRepository;
        public IndexModel(ITodoRepository todoRepository)
        {
            this.todoRepository = todoRepository;
        }
        public IEnumerable<Todo> Todos { get; private set; }

        public async Task OnGetAsync()
        {
            await GetTodos();
        }

        public async Task<IActionResult> OnPostAsync(Todo todo)
        {
            await OnGetAsync();
            if (ModelState.IsValid)
            {
                await todoRepository.Add(todo);
                return RedirectToPage();
            } else
            {
                await GetTodos();
                return Page();
            }
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                await todoRepository.Remove(id);
                return RedirectToPage();
            } else
            {
                return Page();
            }
        }
        private async Task GetTodos()
        {
            Todos = await todoRepository.GetAll();
        }
    }
}

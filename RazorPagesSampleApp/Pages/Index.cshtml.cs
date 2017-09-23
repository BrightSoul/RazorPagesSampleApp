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
        private readonly ITodoRepository _todoRepository;
        public IndexModel(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }
        [TempData]
        public string Message { get; set; }

        [BindProperty]
        public IEnumerable<Todo> Todos { get; private set; }

        public async Task OnGetAsync()
        {
            await GetTodos();
        }

        public async Task<IActionResult> OnPostAsync(Todo todo)
        {
            await OnGetAsync();
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _todoRepository.Add(todo);
            Message = $"Added: {todo.Description}";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostRemoveAsync(Guid id)
        {
            if (ModelState.IsValid)
            {
                await _todoRepository.Remove(id);
                return RedirectToPage();
            }
            return Page();
        }
        public async Task<IActionResult> OnPostEditAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            if ((await _todoRepository.Find(id) != null))
                return RedirectToPage("Edit", new { id = id });

            return Page();
        }
        private async Task GetTodos()
        {
            Todos = await _todoRepository.GetAll();
        }
    }
}

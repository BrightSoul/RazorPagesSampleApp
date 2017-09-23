using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RazorPagesSampleApp.Models;

namespace RazorPagesSampleApp.Pages
{
    public class EditModel : PageModel
    {
        private readonly ITodoRepository _todoRepository;

        public EditModel(ITodoRepository todoRepository)
        {
            _todoRepository = todoRepository;
        }

        public Todo EditTodo { get; private set; }

        public async Task OnGetAsync(Guid id)
        {
            await GetEditTodo(id);
        }

        public async Task<IActionResult> OnPostAsync(Todo todo)
        {
            if (ModelState.IsValid)
            {
                await _todoRepository.Update(todo);
                return RedirectToPage("Index");
            }
            return RedirectToPage();
        }

        private async Task GetEditTodo(Guid id)
        {
            EditTodo = await _todoRepository.Find(id);
        }
    }
}
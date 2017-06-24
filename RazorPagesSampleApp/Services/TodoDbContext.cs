using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RazorPagesSampleApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace RazorPagesSampleApp.Services
{
    public class TodoDbContext : DbContext, ITodoRepository
    {
        public DbSet<Todo> Todos { get; private set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>().ToTable("Todo").HasKey(todo => todo.Id);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("Todo");
        }

        async Task ITodoRepository.Add([Bind(nameof(Todo.Description))] Todo todo)
        {
            Todos.Add(todo);
            await SaveChangesAsync();
        }

        async Task<IEnumerable<Todo>> ITodoRepository.GetAll()
        {
            return await Todos.ToListAsync();
        }

        async Task ITodoRepository.Remove(Guid id)
        {
            var todo = await Todos.FindAsync(id);
            Todos.Remove(todo);
            await SaveChangesAsync();
        }
    }
}

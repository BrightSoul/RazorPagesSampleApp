using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesSampleApp.Models
{
    public class Todo
    {
        public Todo()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; private set; }

        [Required]
        public string Description { get; set; }
    }
}

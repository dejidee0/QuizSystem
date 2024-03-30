using System;
using System.ComponentModel.DataAnnotations;

namespace QuizAppSystem.Models
{
    public class Category
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        public string Name { get; set; }
    }
}


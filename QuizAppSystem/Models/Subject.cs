using System;
using System.ComponentModel.DataAnnotations;

namespace QuizAppSystem.Models
{
    public class Subject
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }
    }
}

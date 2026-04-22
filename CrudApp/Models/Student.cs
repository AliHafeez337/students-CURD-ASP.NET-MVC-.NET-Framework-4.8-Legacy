using System.ComponentModel.DataAnnotations;

namespace CrudApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(256)] // ← Add this line
        public string Email { get; set; }

        [Required]
        public string Grade { get; set; }
    }
}
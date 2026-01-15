using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class AssignTask
    {
        [Key]
        public int Id { get; set; } // Primary key

        [Required]
        public int EmployeeId { get; set; } // FK to Employee

        [Required]
        public int TaskId { get; set; } // FK to Tasks

        [Required]
        public DateTime DateAssigned { get; set; } // Date of assignment

        [Required]
        public string Status { get; set; } = "Pending"; // default
        
        [Required]
        public int DurationDays { get; set; } = 1;

        [Required]
        [MaxLength(150)]
        

        // Navigation properties
        [ForeignKey(nameof(EmployeeId))]
        public Employee Employee { get; set; }

        [ForeignKey(nameof(TaskId))]
        public Tasks Task { get; set; }

        
    }
}

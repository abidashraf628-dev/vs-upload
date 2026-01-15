using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Employee
    {
        public int Id { get; set; } // Primary Key

        [Required]
        public int CompanyId { get; set; }

        [Required]
        public int UnitId { get; set; }

        public DateTime CreatedAt { get; set; }
        public int CreatedBy { get; set; }
        public DateTime UpdatedAt { get; set; }
        public int UpdatedBy { get; set; }

        [Required]
        public int EmployeeId { get; set; } // Business Employee ID (Unique)

        [Required]
        [MaxLength(150)]
        public string FullName { get; set; }

        public bool Status { get; set; }

        // Navigation properties
        [ForeignKey(nameof(CompanyId))]
        public Company Company { get; set; }

        [ForeignKey(nameof(UnitId))]
        public Unit Unit { get; set; }


       
    }
}



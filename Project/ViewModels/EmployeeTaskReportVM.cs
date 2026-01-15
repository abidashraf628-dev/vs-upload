using Project.Models;

namespace Project.ViewModels
{
    public class EmployeeTaskReportVM
    {
        public int? EmployeeId { get; set; }
        public string? Status { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        public List<AssignTask> Results { get; set; } = new();
    }
}

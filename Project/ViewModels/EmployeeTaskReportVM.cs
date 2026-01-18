using Project.Models;
using Project.Models;
using System;
using System.Collections.Generic;

namespace Project.ViewModels
{
    public class EmployeeTaskReportVM
    {
        public int? EmployeeId { get; set; }        // Selected Employee filter
        public string Status { get; set; }          // Selected Status filter
        public DateTime? FromDate { get; set; }     // Filter From Date
        public DateTime? ToDate { get; set; }       // Filter To Date

        // Resulting filtered tasks
        public List<AssignTask> Results { get; set; } = new List<AssignTask>();
    }
}

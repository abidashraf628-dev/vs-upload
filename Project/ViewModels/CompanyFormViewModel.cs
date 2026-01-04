using Project.Models;
using System.Collections.Generic;

namespace Project.ViewModels
{
    public class CompanyFormViewModel
    {
        public Company Company { get; set; }             // For Add/Edit form
        public IEnumerable<Company> Companies { get; set; } // Table data
    }
}

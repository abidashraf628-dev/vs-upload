using Project.Models;
using System.Collections.Generic;

namespace Project.ViewModels
{
    public class UnitFormViewModel
    {
        public Unit Unit { get; set; }                  // For Add/Edit form
        public IEnumerable<Company> Companies { get; set; } // Dropdown
        public IEnumerable<Unit> Units { get; set; }        // Table display
    }
}

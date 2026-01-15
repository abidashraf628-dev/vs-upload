public class EmployeeViewModel
{
    public int Id { get; set; }
    public string Company { get; set; }
    public string Unit { get; set; }
    public int EmployeeId { get; set; }
    public string FullName { get; set; }
    public bool Status { get; set; }
    public int CompanyId { get; set; }
    public int UnitId { get; set; }

    public DateTime CreatedAt { get; set; }
    public int CreatedBy { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int UpdatedBy { get; set; }
}

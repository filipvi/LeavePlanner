namespace LeavePlanner.Models.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string UserRole { get; set; }
        public string LeaveDaysPerYear { get; set; }
        public string RemainingDays { get; set; }
    }
}

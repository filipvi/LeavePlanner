namespace LeavePlanner.Models.ViewModels.Employee
{
    public class DetailsEmployeeLeavesViewModel
    {
        public int Id { get; set; }
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Status { get; set; }
        public string WorkingDaysUsed { get; set; }
        public int StatusId { get; set; }
    }
}

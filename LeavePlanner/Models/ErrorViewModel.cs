namespace LeavePlanner.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string Url { get; set; }
        public string UserName { get; set; }
        public string Message { get; set; }
    }
}

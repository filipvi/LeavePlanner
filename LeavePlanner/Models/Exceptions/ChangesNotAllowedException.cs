namespace LeavePlanner.Models.Exceptions
{
    public class ChangesNotAllowedException : Exception
    {
        public ChangesNotAllowedException(string message) : base(message)
        {

        }
    }
}

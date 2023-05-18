namespace LeavePlanner.Models.Exceptions
{
    public class CreateNotAllowedException : Exception
    {
        public CreateNotAllowedException(string message) : base(message)
        {

        }
    }
}

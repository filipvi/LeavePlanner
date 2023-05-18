namespace LeavePlanner.Utilities.Extensions
{
    public static class RequestExtensions
    {
        private const string HeaderKey = "X-Requested-With";
        private const string HeaderValue = "XMLHttpRequest";

        public static bool IsAjaxRequest(this HttpRequest request)
        {
            var isAjaxRequest = request.Headers[HeaderKey] == HeaderValue;

            return isAjaxRequest;
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace LeavePlanner.Utilities.Logger
{
    public static class Log
    {
        private static readonly ILogger Logger = ApplicationLogging.CreateLogger("AppLogger");

        public static void ControllerLog(Controller controller, Exception e, int? id)
        {
            try
            {
                string error;
                if (id == null)
                {
                    error = $"{Environment.NewLine}CONTROLLER LOG {Environment.NewLine}" +
                            $"Date and time: {DateTime.Now} {Environment.NewLine}" +
                            $"Message: {e.Message} {Environment.NewLine}" +
                            $"Controller: {controller.ControllerContext.RouteData.Values["controller"]} {Environment.NewLine}" +
                            $"Action: {controller.ControllerContext.RouteData.Values["action"]} {Environment.NewLine}" +
                            $"Full exception: {e} ";
                }
                else
                {
                    error = $"{Environment.NewLine}CONTROLLER LOG {Environment.NewLine}" +
                            $"Date and time: {DateTime.Now} {Environment.NewLine}" +
                            $"Message: {e.Message} {Environment.NewLine}" +
                            $"Controller: {controller.ControllerContext.RouteData.Values["controller"]} {Environment.NewLine}" +
                            $"Action: {controller.ControllerContext.RouteData.Values["action"]} {Environment.NewLine}" +
                            $"Id: {id} {Environment.NewLine}" +
                            $"Full exception: {e}";
                }

                Logger.LogError(error);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static void RepositoryLog(string repository, string method, Exception e, int? id)
        {
            try
            {
                string error;
                if (id == null)
                {
                    error = $"{Environment.NewLine}REPOSITORY LOG {Environment.NewLine}" +
                            $"Date and time: {DateTime.Now} {Environment.NewLine}" +
                            $"Message: {e.Message} {Environment.NewLine}" +
                            $"Repository: {repository} {Environment.NewLine}" +
                            $"Method: {method} {Environment.NewLine}" +
                            $"Full exception: {e} ";
                }
                else
                {
                    error = $"{Environment.NewLine}REPOSITORY LOG {Environment.NewLine}" +
                            $"Date and time: {DateTime.Now} {Environment.NewLine}" +
                            $"Message: {e.Message} {Environment.NewLine}" +
                            $"Repository: {repository} {Environment.NewLine}" +
                            $"Method: {method} {Environment.NewLine}" +
                            $"Id: {id} {Environment.NewLine}" +
                            $"Full exception: {e} ";
                }
                Logger.LogError(error);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public static void InfoLog(string message)
        {
            var infoMessage = new StringBuilder();
            infoMessage.AppendLine();
            infoMessage.Append("INFO LOG");
            infoMessage.AppendLine();
            infoMessage.Append("Date time: " + DateTime.Now);
            infoMessage.AppendLine();
            infoMessage.Append("Message: " + message);

            Logger.LogInformation(infoMessage?.ToString());
        }

        public static void ErrorLog(string message)
        {
            var errorMessage = new StringBuilder();

            errorMessage.AppendLine();
            errorMessage.Append("ERROR:");
            errorMessage.AppendLine();
            errorMessage.Append("Date time: " + DateTime.Now);
            errorMessage.AppendLine();
            errorMessage.Append("Message: " + message);

            Logger.LogError(errorMessage?.ToString());
        }
    }
}

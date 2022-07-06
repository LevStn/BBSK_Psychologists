using Microsoft.AspNetCore.Mvc;

namespace BBSK_Psycho.Extensions
{

    public static class ControllerExtensions
    {
        public static string GetRequestPath(this ControllerBase controller) =>
            $"{controller.Request?.Scheme}://{controller.Request?.Host.Value}{controller.Request?.Path.Value}";
    }
}
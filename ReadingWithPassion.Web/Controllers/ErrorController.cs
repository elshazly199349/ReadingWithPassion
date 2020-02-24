using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ReadingWithPassion.Web
{
    public class ErrorController : Controller
    {
        private readonly ILogger _logger;
        public ErrorController(ILogger<ErrorController> logger) {
            _logger = logger;
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMesssage = "Sorrr, the resource you have asked couldn't be found";
                    break;

                default:
                    ViewBag.ErrorMessage = "Error has happend";
                    break;
            }
            _logger.LogWarning($"404 Error occured. Path={statusCodeResult.OriginalPath}" +
                $"and QueryString={statusCodeResult.OriginalQueryString}");
            return View();
        }

        [Route("Error")]
        [AllowAnonymous]
        public IActionResult Error() {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError($"The Path {exceptionDetails.Path} throw an exception {exceptionDetails.Error}");
            return View("Error");
        }   
    }
}
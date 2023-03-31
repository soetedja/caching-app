using Api.Dto;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebServerController : ControllerBase
    {
        private readonly ILogger<WebServerController> _logger;
        private readonly IWebServer _webServer;

        public WebServerController(ILogger<WebServerController> logger, IWebServer webServer)
        {
            _logger = logger;
            _webServer = webServer;
        }

        [HttpPost]
        public int WebServer(RequestDto request)
        {
            return _webServer.GetModNumber(request.Number);
        }
    }
}

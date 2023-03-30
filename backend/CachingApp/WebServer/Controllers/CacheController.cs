using Api.Dto;
using Api.Models;
using Api.Services;
using Common;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CacheController : ControllerBase
    {
        private readonly ILogger<CacheController> _logger;
        private readonly IWebServer _webServer;

        public CacheController(ILogger<CacheController> logger, IWebServer webServer)
        {
            _logger = logger;
            _webServer = webServer;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var cache = _webServer.GetCache();

            var res = new CacheStatisticDto()
            {
                TotalHits = cache.Hits,
                TotalMisses = cache.Misses,
                MemoryLayout = cache.MemoryAddressLayout,
                //CacheAuditLogs = cache.CacheAuditLogs,
                HitFrequency = cache.GetFrequency()
            };

            return Ok(res);
        }
    }
}
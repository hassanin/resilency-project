using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ResilencyClient.Config;
using System.Text.Json;

namespace ResilencyClient.Controllers
{
    [Route("api/simulation")]
    [ApiController]
    public class SimulationController : ControllerBase
    {
        private readonly ILogger<SimulationController> _logger;
        private readonly ApplicationConfig _applicationConfig;
        private readonly IHttpClientFactory _httpClientFactory;
        public SimulationController(IOptions<ApplicationConfig> applicationConfig,
            ILogger<SimulationController> logger,
            IHttpClientFactory httpClientFactory
            )
        {
            _applicationConfig = applicationConfig.Value;
            Console.WriteLine(JsonSerializer.Serialize(_applicationConfig));
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }
        [Route("start")]
        [HttpGet]
        public string Start()
        {
            return " start";
        }

        [Route("viable")]
        [HttpGet]
        public async Task<string> CallBackend()
        {
            _logger.LogInformation("Hello from Callbackend");
            var client = _httpClientFactory.CreateClient("Basic");
            var res = await client.GetAsync("api/primary/hi");
            _logger.LogInformation(await res.Content.ReadAsStringAsync());
            return "finished";
        }
    }
}

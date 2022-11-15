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
        private readonly ApplicationConfig _applicationConfig;
        public SimulationController(IOptions<ApplicationConfig> applicationConfig)
        {
            _applicationConfig = applicationConfig.Value;
            Console.WriteLine(JsonSerializer.Serialize(_applicationConfig));
        }
        [Route("start")]
        [HttpGet]
        public string Start()
        {
            return " start";
        }
    }
}

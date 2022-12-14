using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using System.Threading.Tasks;

namespace ToggleApimCircuit
{
    public static class Function1
    {
        [FunctionName("Function1")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = "function1")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string name = req.Query["name"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            name = name ?? data?.name;

            string responseMessage = string.IsNullOrEmpty(name)
                ? "This HTTP triggered function executed successfully. Pass a name in the query string or in the request body for a personalized response."
                : $"Hello, {name}. This HTTP triggered function executed successfully.";

            return new OkObjectResult(responseMessage);
        }

        [FunctionName("Health")]
        public static async Task<IActionResult> Health(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "health")] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            return new OkObjectResult("Health v12");
        }
        [FunctionName("OpenCircuit")]
        public static async Task<IActionResult> OpenCircuit(
           [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "open")] HttpRequest req,
           ILogger log)
        {
            log.LogInformation("In Open Circuit");
            await ApimManager.GetCircuitState(log);
            await ApimManager.UpdateAPImanagment(true, log);
            await ApimManager.GetCircuitState(log);
            return new OkObjectResult("Health v3");
        }
        [FunctionName("CloseCircuit")]
        public static async Task<IActionResult> CloseCircuit(
          [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "close")] HttpRequest req,
          ILogger log)
        {
            log.LogInformation("In Close Circuit");
            await ApimManager.GetCircuitState(log);
            await ApimManager.UpdateAPImanagment(false, log);
            await ApimManager.GetCircuitState(log);
            return new OkObjectResult("Health v4");
        }
    }
}

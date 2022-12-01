using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers.Dummy
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        /// <summary>
        ///  Adds two numbers via a GET HTTP action
        /// </summary>
        /// <param name="x"> First operand</param>
        /// <param name="y"> Second Operand</param>
        /// <returns>A <see cref="Task{TResult}"/> representing the result of the asynchronous operation.</returns>
        [Route("")]
        [HttpGet]
        public async Task<double> Test123([FromQuery(Name = "subscription-key")] string subscriptionKey, [FromQuery(Name = "query")] string query)
        {
            Console.WriteLine($"received {subscriptionKey} and {query}");
            return 42;
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using web_api.Models;

namespace web_api.Controllers
{
    [Route("api/factorial")]
    [ApiController]
    public class FactorialController : ControllerBase
    {
        [Route("{number}")]
        [HttpGet]
        public async Task<string> GetFactorial(int number)
        {
            await Task.Delay(100);
            return Factorial(number).ToString();
        }

        [Route("post")]
        [HttpPost]
        public async Task<FactorialResponse> GetFactorial2([FromBody] FactorialRequest factorialRequest)
        {
            await Task.Delay(100);
            var factorialResult = Factorial(factorialRequest.Number);
            var result = new FactorialResponse() { Result = factorialResult, version = "1.0" };
            return result;
        }
        public static int Factorial(int number)
        {
            if (number == 1)
            {
                return 1;
            }
            else
            {
                return number * Factorial(number - 1);
            }
        }
    }
}

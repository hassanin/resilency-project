using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using web_api.Config;
using web_api.State;

namespace web_api.Controllers
{
    [Route("api/primary")]
    [ApiController]
    public class PrimaryController : ControllerBase
    {
        private readonly ResponseArray _responseArray;
        private readonly IHighLevelStateManager _highLevelStateManager;
        private readonly Semaphore _semaphore;

        public PrimaryController(IOptions<ResponseArray> responseArray,
            IHighLevelStateManager highLevelStateManager)
        {
            _responseArray = responseArray.Value;
            _highLevelStateManager = highLevelStateManager;
            var maxCount = _responseArray.ResponseProfiles[0].NumRequests;
            Console.WriteLine($"max count is {maxCount}");
            _semaphore = new(maxCount, maxCount);
        }

        [HttpGet]
        [Route("hi")]
        public async Task<string> Hello()
        {
            if (_highLevelStateManager.PrimaryEndpointActive)
            {
                Console.WriteLine("Before wait one");
                _semaphore.WaitOne();

                Console.WriteLine("After wait one");
                int waitTime = _responseArray.ResponseProfiles[0].ResponseTimeMilliSeconds;
                await Task.Delay(waitTime);
                _semaphore.Release();
                return $"Hello I waited {waitTime} milliSeconds";
            }
            else
            {
                Response.StatusCode = 502;
                return "I am out of service";
            }
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using web_api.Config;

namespace web_api.Controllers
{
    [Route("api/location")]
    [ApiController]
    public class LocationsController : ControllerBase
    {
        private readonly ResponseArray _responseArray;
        [HttpGet]
        [Route("hi")]
        public async Task<string> Response()
        {
            int waitTime = _responseArray.ResponseProfiles[0].ResponseTimeMilliSeconds;
            await Task.Delay(waitTime);
            return $"Hello I waited {waitTime} milliSeconds";
        }
        public LocationsController(IOptions<ResponseArray> responseArray)
        {
            _responseArray = responseArray.Value;
        }
    }
}

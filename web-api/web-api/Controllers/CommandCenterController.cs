using Microsoft.AspNetCore.Mvc;
using web_api.State;

namespace web_api.Controllers
{
    [Route("api/control")]
    [ApiController]
    public class CommandCenterController : ControllerBase
    {
        private readonly IHighLevelStateManager _highLevelStateManager;
        public CommandCenterController(IHighLevelStateManager highLevelStateManager)
        {
            _highLevelStateManager = highLevelStateManager;
        }
        //TODO: make post
        /// <summary>
        /// Enables/Disbales request processing for the Primary Controller
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("toggle")]
        public async Task<string> Toggle()
        {
            var currentState = _highLevelStateManager.PrimaryEndpointActive;
            _highLevelStateManager.ToggleEndpoint();
            var newState = _highLevelStateManager.PrimaryEndpointActive;
            return $"old state was {currentState} and new state is {newState}";
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace web_api.Controllers
{
    [Route("api/backup")]
    [ApiController]
    public class BackupController : ControllerBase
    {
        [HttpGet]
        [Route("hi")]
        public async Task<string> Location()
        {
            return await Task.FromResult("done");
        }
    }
}

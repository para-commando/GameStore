using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers
{
    [ApiExplorerSettings(GroupName = "v2")]
    [Route("api/[controller]")]
    public class TwoController : ControllerBase
    {
        // Update the route path for clarity
        [HttpGet("get-all-games-v2")]
        public IActionResult GetGames() => Ok(new[] { "Game 1", "Game 2" });
    }
}

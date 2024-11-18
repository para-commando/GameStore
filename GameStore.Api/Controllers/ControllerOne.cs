using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers
{
    [ApiExplorerSettings(GroupName = "v1")]
    [Route("api/[controller]")]
    public class OneController : ControllerBase
    {
        // Update the route path for clarity
        [HttpGet("get-all-games-v1")]
        public IActionResult GetGames() => Ok(new[] { "Game 1", "Game 2" });
    }
}

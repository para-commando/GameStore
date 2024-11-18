using Microsoft.AspNetCore.Mvc;

namespace GameStore.Api.Controllers
{
  [Route("api/[controller]")]
  public class CommonController : ControllerBase
  {
    // Update the route path for clarity
    [HttpGet("common-controller")]
    public IActionResult GetGames() => Ok(new[] { "Game 1", "Game 2" });
  }
}

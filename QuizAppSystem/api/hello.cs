using Microsoft.AspNetCore.Mvc;

namespace QuizAppSystem.api
{
    [ApiController]
    [Route("api/[controller]")]
    public class HelloController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Hello from .NET Core API!");
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace PORTALE_SPESE_MS_AUTH.Controllers
{
    [Authorize]
    [ApiController]
    [Route("protected/[controller]")]
    public class ObtainUsersController : ControllerBase
    {
        [HttpGet("data")]
        public IActionResult GetProtectedData()
        {
            return Ok(new { message = "Sei autenticato!", user = User.Identity.Name });
        }
    }
}

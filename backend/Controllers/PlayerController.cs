using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [ApiController]
    [Route("/player")]
    public class PlayerController : ControllerBase
    {

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CreatePlayer(string pet)
        {
            var result = new { message = "player made", pet };
            return StatusCode(StatusCodes.Status201Created, result);
        }
    }
}

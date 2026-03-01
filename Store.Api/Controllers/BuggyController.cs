using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Api.Errors;

namespace Store.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        [HttpGet ("NotFound")]
        public IActionResult GetNotFoundErrors()
        {
            return NotFound(new ApiErrorResponse (404));
        }

        [HttpGet("BadRequest")]
        public IActionResult GetBadRequestErors()
        {
            return BadRequest(new ApiErrorResponse(400));
        }

        [HttpGet("UnAuthorized")]
        public IActionResult UnAuthorizedErors()
        {

            return Unauthorized(new ApiErrorResponse(401));
        }

        [HttpGet("BadRequest/{id}")]
        public IActionResult GetBadRequestErors(int id)
        {
            return Ok();
        }

        [HttpGet("ServerError")]
        public IActionResult GetServerError()
        {
            
            string? name = null;
            var length = name.Length; 

            return Ok();
        }

    }
}

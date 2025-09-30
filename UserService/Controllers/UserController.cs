using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using User.Application.Services;
using User.Domain.Models.Response;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET: api/<UserController>
        [Authorize]
        [HttpGet]
        public ActionResult<UserResponseDTO> GetUserData()              //public async Task<ActionResult>
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
            {
                return Unauthorized(new { error = "No authorization", code = (int)HttpStatusCode.Unauthorized });
            }

            int userId = int.Parse(userIdClaim);
            try
            {
                var userDto = _userService.GetUser(userId);
                if (userDto == null)
                {
                    return NotFound(new { error = "User not found", code= (int)HttpStatusCode.NotFound});
                }
                return Ok(userDto);
            }
            catch (Exception ex) 
            {
                return NotFound(new { error = ex.Message, code = (int)HttpStatusCode.NotFound});
            }

        }
    }
}

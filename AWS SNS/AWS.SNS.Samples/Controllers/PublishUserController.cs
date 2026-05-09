using AWS.SNS.Samples.Model.Dtos;
using AWS.SNS.Samples.Service;
using Microsoft.AspNetCore.Mvc;

namespace AWS.SNS.Samples.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PublishUserController : ControllerBase
    {

        private readonly ILogger<PublishUserController> _logger;
        private readonly IUserService _userService;

        public PublishUserController(ILogger<PublishUserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost]
        public async Task<ActionResult> PublishUser(UserDto user) 
        {
            try
            {
                await _userService.RegisterUserAsync(user);

                return Created($"Successfully published user Name:{user.Name} Email:{user.Email}", null);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

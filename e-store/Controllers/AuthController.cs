using e_store.Models;
using Microsoft.AspNetCore.Mvc;

namespace e_store.Controllers
{
    [Route("Api/[Controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public readonly IAuthService _authservice;
        public AuthController(IAuthService authService)
        {
            _authservice = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> RegisterAsync(RegisterModel model)
        {
            if (!ModelState.IsValid) {
            return BadRequest(ModelState);
            }
            var result= await _authservice.RejesterAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("GetToken")]
        public async Task<IActionResult> GetTokenAsync(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _authservice.GetTokenAsync(model);
            if (!result.IsAuthenticated)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

    }
}

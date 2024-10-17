using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TicketPurchaseAPI.Dto.Account;
using TicketPurchaseAPI.Model;

namespace TicketPurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var appUser = new AppUser
                {
                    Email = registerDto.Email,
                    UserName = registerDto.Username,
                    PasswordHash = registerDto.Password
                };
                
                var createdUser = await  _userManager.CreateAsync(appUser,registerDto.Password);
                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(appUser, "User");
                    if (roleResult.Succeeded)
                    {
                        return Ok("Account Registered Successfully");
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else 
                { 
                    return StatusCode(500, "Error registering account"); 
                }
            }
            catch (Exception e) 
            {
                return StatusCode(500, e);
            }
        } 
    }
}

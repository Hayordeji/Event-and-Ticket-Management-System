using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketPurchaseAPI.Dto.Account;
using TicketPurchaseAPI.Model;
using TicketPurchaseAPI.Services;

namespace TicketPurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenservice;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenservice = tokenService;
            _signInManager = signInManager;
        }

        [HttpPost("Register")]
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
                        return Ok(new NewUserDto
                        {
                            Email = appUser.Email,
                            Username = appUser.UserName,
                            JwtToken = await _tokenservice.CreateTokenAsync(appUser)
                        });
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName  == loginDto.EmailOrUsername.ToLower() || u.Email == loginDto.EmailOrUsername.ToLower());
            if (user == null)
            {
                return Unauthorized("Email/Username is incorrect");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password,false);
            if (!result.Succeeded)
            {
                return Unauthorized("Password is incorrect");
            }
            return Ok(new NewUserDto
            {
                Email = user.Email,
                Username = user.UserName,
                JwtToken = await _tokenservice.CreateTokenAsync(user)
            });
        }
    }
}

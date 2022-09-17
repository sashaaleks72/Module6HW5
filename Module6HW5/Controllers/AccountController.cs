using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Module6HW5.Interfaces;
using Module6HW5.Models;
using Module6HW5.ViewModels.AuthModels;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Module6HW5.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> SignIn([FromBody] LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var foundUser = await _accountService.FindAccount(loginModel);

                if (foundUser != null)
                {
                    await Authenticate(foundUser);
                    return Ok(new { SuccessMessage = "Successfully logined!" });
                }
                else
                {
                    return BadRequest(new { ErrorMessage = "Account with these login and password wasn't found!" });
                }
            }

            return BadRequest(ModelState);
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                var createdUser = await _accountService.CreateNewAccount(registerModel);

                if (createdUser != null)
                {
                    await Authenticate(createdUser);
                    return Ok(new { SuccessMessage = "Successfully signed up!" });
                }
                else
                {
                    return BadRequest(new { ErrorMessage = "Account with this email already exists!" });
                }
            }

            return BadRequest(ModelState);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpPost("logout")]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}

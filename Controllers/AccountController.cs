using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SBTBackEnd.Entities;
using SBTBackEnd.Entities.DTO;
using SBTBackEnd.Error;
using SBTBackEnd.Extensions;
using SBTBackEnd.Services.TokenService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SBTBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<SBTUser> _userManager;
        private readonly SignInManager<SBTUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<SBTUser> userManager,SignInManager<SBTUser> signInManager,ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpGet("GetCurrentUser")]
        public async Task<ActionResult<SBTUserDto>> GetCurrentUser()
        {
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await _userManager.FindByEmailFromClaimsPrincipal(HttpContext.User);
            return new SBTUserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<SBTUserDto>> Login(UserLoginDto userLogin)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            if (user == null) return Unauthorized(new ApiResponse(401));
            var result = await _signInManager.CheckPasswordSignInAsync(user, userLogin.Password, false);
            if (!result.Succeeded) return Unauthorized(new ApiResponse(401));
            return new SBTUserDto
            {
                Email = user.Email,
                Token = _tokenService.CreateToken(user),
                DisplayName = user.DisplayName
            }; 
        }

        [HttpPost("register")]
        public async Task<ActionResult<SBTUserDto>> Register(UserRegisterDto userRegister)
        {
            if (CheckEmailExistsAsync(userRegister.Email).Result.Value)
            {
                return new BadRequestObjectResult(new ApiValidationResponse {Errors =new[] {"Email is in use"} });
            }
            var user = new SBTUser
            {
                DisplayName = userRegister.DisplayName,
                UserName=userRegister.DisplayName,
                Email = userRegister.Email,
                PhoneNumber=userRegister.mobile,
                
            };
            var result = await _userManager.CreateAsync(user, userRegister.Password);
            if (!result.Succeeded) return BadRequest(new ApiResponse(400));
            
            return new SBTUserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            };
        }

        [HttpGet("emailexists")]
        private async Task<ActionResult<bool>> CheckEmailExistsAsync([FromQuery]string email)
        {
            return await _userManager.FindByEmailAsync(email) != null;
        }


        #region ExternalLogin
        [HttpPost("ExternalLogin")]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallBack", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpGet]
        public async Task<ActionResult<SBTUserDto>> ExternalLoginCallBack(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogin userLoginDto = new ExternalLogin
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };
            
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return Unauthorized(new ApiResponse(401));
            }

            var email = info.Principal.FindFirstValue(ClaimTypes.Email);
            SBTUser user = null;
            if (email != null)
            {
                user = await _userManager.FindByEmailAsync(email);
            }
            var signinResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: true, bypassTwoFactor: true);
            if (signinResult.Succeeded)
            {
                return StatusCode(201) ;
            }
            else
            {
                if (email != null)
                {
                    if (user == null)
                    {
                        user = new SBTUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Name),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email)

                        };
                        await _userManager.CreateAsync(user);
                        //var token = await _userManager.G;

                    }
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);
                }
                if (user == null)
                    return BadRequest("Invalid External Authentication.");
            }
            return new SBTUserDto
            {
                DisplayName = user.DisplayName,
                Token = _tokenService.CreateToken(user),
                Email = user.Email
            }; ;
        }
        #endregion


    }
}

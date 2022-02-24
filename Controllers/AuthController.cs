using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using SBTBackEnd.Models;
using SBTBackEnd.Models.DTO;
using SBTBackEnd.Services.AuthService;

namespace SBTBackEnd.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthRepository _repository;
		private readonly IConfiguration _config;

		public AuthController(IAuthRepository repository,IConfiguration config )
		{
			_repository = repository;
			_config = config;
		}

		[HttpPost("register")]
		public async Task<ActionResult<User>> Register(UserRegisterDTO userRegisterDto)
		{
			userRegisterDto.email = userRegisterDto.email;
			if (await _repository.UserExists(userRegisterDto.email))
			{
				return BadRequest("This user Exists before");
			}
			var userToCreate = new User
			{
				Username = userRegisterDto.email,
			};
			var createUser = await _repository.Register(userToCreate, userRegisterDto.password);
			return StatusCode(201);
		}

		[HttpPost("login")]
		public async Task<ActionResult<User>> Login(UserLoginDTO userLoginDTO)
		{
			
			var userFromRepo = await _repository.Login(userLoginDTO.userName.ToLower(), userLoginDTO.password);
			if (userFromRepo ==null)
			{
				return Unauthorized();
			}
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
				new Claim(ClaimTypes.Name,userFromRepo.Username)
			};
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("Token:Key").Value));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject=new ClaimsIdentity(claims),
				Expires=DateTime.Now.AddDays(1),
				SigningCredentials=creds
			};
			var tokenHandler = new JwtSecurityTokenHandler();
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return Ok(new
			{
				token = tokenHandler.WriteToken(token)
			});
		}
	}
}

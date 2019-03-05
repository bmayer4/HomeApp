using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HomeApp.API.Data;
using HomeApp.API.Dtos;
using HomeApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace HomeApp.API.Controllers
{
    
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;
        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, AppDbContext context, IConfiguration config)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _context = context;
            _config = config;
        }

        [HttpGet("users/{id}", Name = "GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            // TODO - change code when we add repo
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return NotFound();
            }

             var userToReturn = _mapper.Map<UserToReturnDto>(user);

            
            return Ok(userToReturn);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] UserForRegisterDto userForRegisterDto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDto>(userToCreate);
                await _userManager.AddToRoleAsync(userToCreate, "Member");
                return CreatedAtRoute("GetUser", new { id = userToCreate.Id }, userToReturn);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserForLoginDto userForLoginDto)
        {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            var user = await _userManager.FindByNameAsync(userForLoginDto.UserName);

            if (user == null) 
            {
                return UnprocessableEntity("Invalid Username or Password");
            }

            // invalid username returns 500 server error without above check
            var result = await _signInManager.CheckPasswordSignInAsync(user, userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var userToReturn = _mapper.Map<UserToReturnDto>(user);
                return Ok(new {
                    user = userToReturn,
                    token = await GenerateJwtToken(user)
                });
            }

            return UnprocessableEntity("Invalid Username or Password");  // stack overflow recommends 422 for failed login credentials
        }

        private async Task<string> GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),  //in token as nameid
                new Claim(ClaimTypes.Name, user.UserName)  //in token as unique_name
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
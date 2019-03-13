using System.Threading.Tasks;
using HomeApp.API.Data;
using HomeApp.API.Models;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeApp.API.Dtos;
using System;

namespace HomeApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController: ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public AdminController(AppDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("usersWithRoles")]
        public async Task<IActionResult> GetUsersWithRoles()
         {
             var usersWithRoles = await (from user in _context.Users orderby user.Email
                                            select new {
                                                Id = user.Id,
                                                Email = user.Email, 
                                                Roles = (from userRole in user.UserRoles
                                                    join role in _context.Roles on userRole.RoleId equals role.Id
                                                    select role.Name).ToList()
                                        }).ToListAsync();

            return Ok(usersWithRoles);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPatch("editRoles/{id}")]
        public async Task<IActionResult> EditUserRole(int id, [FromBody] RoleEditDto roleEditDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userFromRepo = await _userManager.FindByIdAsync(id.ToString());

            if (userFromRepo == null)
            {
                return BadRequest();
            }

            var userRoles = await _userManager.GetRolesAsync(userFromRepo);

            var allRoles = await _roleManager.Roles.Select(r => r.Name).ToListAsync();

            var selectedRoles = roleEditDto.RoleNames;

            foreach (var role in selectedRoles)
            {
                if (!allRoles.Contains(role))
                {
                    return BadRequest("Invalid roles");
                }
            }

            if (selectedRoles.IndexOf("Member") == -1)
            {
                selectedRoles.Add("Member");
            }

            var result = await _userManager.AddToRolesAsync(userFromRepo, selectedRoles.Except(userRoles));

            if (!result.Succeeded)
            {
                return BadRequest("Failed edit roles..");
            }

            result = await _userManager.RemoveFromRolesAsync(userFromRepo, userRoles.Except(selectedRoles));

            if (!result.Succeeded)
            {
                return BadRequest("Failed to edit roles");
            }

            return Ok(await _userManager.GetRolesAsync(userFromRepo));
        }

    }
}
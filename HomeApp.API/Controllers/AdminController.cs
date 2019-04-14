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

        private readonly IHomeRepository _repo;

        public AdminController(
            AppDbContext context, 
            UserManager<User> userManager, 
            RoleManager<Role> roleManager,
            IHomeRepository repo)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _repo = repo;
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
        public async Task<IActionResult> EditUserRoles(int id, [FromBody] RoleEditDto roleEditDto)
        {
            if (roleEditDto == null)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userFromRepo = await _userManager.FindByIdAsync(id.ToString());

            if (userFromRepo == null)
            {
                return NotFound();
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

            if (!selectedRoles.Contains("Member"))
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

        [Authorize(Policy = "RequireModeratorRole")]
        [HttpDelete("homes/{id}")]
        public async Task<IActionResult> DeleteHomeByModerator(int id)
        {
            var homeFromRepo = await _repo.GetHome(id);

            if (homeFromRepo == null)
            {
                return NotFound();
            }

            _repo.Delete(homeFromRepo);

            if (!await _repo.SaveAll())
            {
                throw new Exception($"Deleting home {id} failed.");
            }

            return NoContent();
        }

    }
}
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using HomeApp.API.Data;
using HomeApp.API.Dtos;
using HomeApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomesController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHomeRepository _repo;

        public HomesController(IMapper mapper, IHomeRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }


        [AllowAnonymous]
        [HttpGet("{id}", Name = "GetHome")]
        public async Task<IActionResult> GetHome(int id)
        {
            var homeFromRepo = await _repo.GetHome(id);

            if (homeFromRepo == null)
            {
                return BadRequest();
            }

            var homeToReturn = _mapper.Map<HomeForDetailDto>(homeFromRepo);

            return Ok(homeToReturn);
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetHomes()
        {
            var homesFromRepo = await _repo.GetHomes();

            var homesToReturn = _mapper.Map<IEnumerable<HomeForListDto>>(homesFromRepo);

            return Ok(homesToReturn);
        }

        [HttpPost]  //not making userId part of url, not part of home functionality
        public async Task<IActionResult> CreateHome([FromBody] HomeForCreationDto homeForCreationDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var user = await _repo.GetUser(userId);

            if (user == null)  // probably not necessary since user is found
            {
                return NotFound();
            }

            var homeEntity = _mapper.Map<Home>(homeForCreationDto);

            user.Homes.Add(homeEntity);

            if (!await _repo.SaveAll())
            {
                throw new Exception("Failed to create home");
            }

            var homeToReturn = _mapper.Map<HomeForDetailDto>(homeEntity);

            return CreatedAtRoute("GetHome", new { id = homeEntity.Id }, homeToReturn);
        }

        [HttpPatch("{id}")]  // or put? (we can use post for changing one property)
        public async Task<IActionResult> UpdateHome(int id, [FromBody] HomeForUpdateDto homeForUpdateDto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

             var user = await _repo.GetUser(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var homeFromRepo = await _repo.GetHome(id);

            if (homeFromRepo == null) {
                return BadRequest();
            }

            if (homeFromRepo.UserId != userId)
            {
                return Unauthorized();
            }

            _mapper.Map(homeForUpdateDto, homeFromRepo);  // every property on dto will update, will be null if left blank (existing properties on Home won't be affected)

            if (!await _repo.SaveAll())
            {
                throw new Exception($"Updating home {id} failed on save."); 
            }
            
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHome(int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

             var user = await _repo.GetUser(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var homeFromRepo = await _repo.GetHome(id);

            if (homeFromRepo == null) {
                return Unauthorized();
            }

            if (homeFromRepo.UserId != userId)
            {
                return Unauthorized();
            }

            _repo.Delete(homeFromRepo);

            if (! await _repo.SaveAll())
            {
                throw new Exception($"Deleting home {id} failed."); 
            }

            return NoContent();
        }
    }
}
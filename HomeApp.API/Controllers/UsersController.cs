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

    public class UsersController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHomeRepository _repo;

         public UsersController(IMapper mapper, IHomeRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}", Name = "GetUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

             var userToReturn = _mapper.Map<UserToReturnDto>(user);
            
            return Ok(userToReturn);
        }

        [HttpPost("favorite/{homeId}")]
        public async Task<IActionResult> ToggleHomeAsFavorite(int homeId)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var user = await _repo.GetUser(userId);

            if (user == null)  
            {
                return Unauthorized();
            }   

            var homeFromRepo = await _repo.GetHome(homeId);

            if (homeFromRepo == null)
            {
                return NotFound();
            }

            var favoriteFromRepo = await _repo.GetFavorite(userId, homeId);

            if (favoriteFromRepo == null)
            {
                var favorite = new Favorite()
                {
                    UserId = userId,
                    HomeId = homeId
                };

                _repo.Add<Favorite>(favorite);
            } 
            else
            {
                _repo.Delete<Favorite>(favoriteFromRepo);
            }

            if (!await _repo.SaveAll())
            {
                throw new Exception($"Adding/removing favorites failed on save."); 
            }
            
            return Ok();

        }   
    }
}
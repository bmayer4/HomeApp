using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using HomeApp.API.Data;
using HomeApp.API.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]

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

        [HttpGet]
        public async Task<IActionResult> GetProfessionalUsers()
        {
            var proUsers = await _repo.GetProfessionalUsers();

            var proUsersToReturn = _mapper.Map<IEnumerable<UserToReturnDto>>(proUsers);

            return Ok(proUsersToReturn);
        }
    }
}
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using HomeApp.API.Data;
using HomeApp.API.Dtos;
using HomeApp.API.Helpers;
using HomeApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace HomeApp.API.Controllers
{
    [Route("api/homes/{homeId}/[controller]")]
    [ApiController]
    public class PhotosController: ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IHomeRepository _repo;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IMapper mapper, IHomeRepository repo, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _mapper = mapper;
            _repo = repo;
            _cloudinaryConfig = cloudinaryConfig;

            Account acc = new Account(
                _cloudinaryConfig.Value.CloudName,
                _cloudinaryConfig.Value.ApiKey,
                _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(acc);
        }

        [AllowAnonymous]
        [HttpGet("{id}", Name="GetPhoto")]
        public async Task<IActionResult> GetPhoto(int homeId, int id)
        {
            var photoFromRepo = await _repo.GetPhoto(homeId, id);

            if (photoFromRepo == null) {
                return NotFound();
            }

            var photoToReturn = _mapper.Map<PhotoForReturnDto>(photoFromRepo);

            return Ok(photoToReturn);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForHome(int homeId, [FromForm] PhotoForCreationDto photoForCreationDto)
        {
            if (photoForCreationDto == null) 
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var user = await _repo.GetUser(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var homeFromRepo = await _repo.GetHome(homeId);

            if (homeFromRepo == null) {
                return NotFound();
            }

            if (homeFromRepo.UserId != userId)
            {
                return Unauthorized();
            }

            var file = photoForCreationDto.File;
            var uploadResult = new ImageUploadResult();  

             if (file.Length > 0) 
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams() 
                     {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill")
                     };

                     uploadResult = _cloudinary.Upload(uploadParams);
                }
            }

            photoForCreationDto.Url = uploadResult.Uri.ToString();
            photoForCreationDto.PublicId = uploadResult.PublicId;

            var photoEntity = _mapper.Map<Photo>(photoForCreationDto);

            if (!homeFromRepo.Photos.Any(p => p.IsCover))
            {
                photoEntity.IsCover = true;
            }

            homeFromRepo.Photos.Add(photoEntity);

             if (!await _repo.SaveAll())
            {
                throw new Exception($"Failed to upload photo."); 
            }

            var photoToReturn = _mapper.Map<PhotoForReturnDto>(photoEntity);
            
            return CreatedAtRoute("GetPhoto", new { homeId = homeId, id = photoEntity.Id }, photoToReturn );
        }

        [HttpPatch("{id}/setCover")]
        public async Task<IActionResult> SetCoverPhoto(int homeId, int id)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var user = await _repo.GetUser(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var homeFromRepo = await _repo.GetHome(homeId);

            if (homeFromRepo == null) {
                return NotFound();
            }

            if (homeFromRepo.UserId != userId)
            {
                return Unauthorized();
            }

            var photoFromRepo = await _repo.GetPhoto(homeId, id);

            if (photoFromRepo == null) {
                return NotFound();
            }

            if (photoFromRepo.IsCover)
            {
                return BadRequest("This is already the cover photo");
            }

            var currentCoverPhoto = await _repo.GetCoverPhotoForHome(homeFromRepo.Id);

            if (currentCoverPhoto == null)
            {
                return NotFound();
            }

            currentCoverPhoto.IsCover = false;

            photoFromRepo.IsCover = true;

            if (!await _repo.SaveAll())
            {
                throw new Exception("Error setting cover photo");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePhoto(int homeId, int id)
        {
           var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            
            var user = await _repo.GetUser(userId);

            if (user == null)
            {
                return Unauthorized();
            }

            var homeFromRepo = await _repo.GetHome(homeId);

            if (homeFromRepo == null) {
                return NotFound();
            }

            if (homeFromRepo.UserId != userId)
            {
                return Unauthorized();
            }

            var photoFromRepo = await _repo.GetPhoto(homeId, id);

            if (photoFromRepo == null) {
                return NotFound();
            }

            if (photoFromRepo.IsCover)
            {
                return BadRequest("Cover photo cannot be deleted");
            }

            var deleteParams = new DeletionParams(photoFromRepo.PublicId);
            var result = _cloudinary.Destroy(deleteParams);

            if (result.Result == "ok") {
                _repo.Delete(photoFromRepo);  
            }

            if (!await _repo.SaveAll())
            {
                throw new Exception($"Failed to delete photo with id of {id}.");  
            }

            return NoContent();

        }

    }
}
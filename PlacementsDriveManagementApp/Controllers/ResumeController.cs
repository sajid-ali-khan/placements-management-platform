using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResumeController: Controller
    {
        private readonly IResumeRepo _resumeRepo;
        private readonly IMapper _mapper;

        public ResumeController(IResumeRepo resumeRepo, IMapper mapper)
        {
            _resumeRepo = resumeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Resume>))]
        public IActionResult GetResumes()
        {
            var resumes = _resumeRepo.GetResumes();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(resumes);
        }

        [HttpGet("{resumeId}")]
        [ProducesResponseType(200, Type = typeof(Resume))]
        [ProducesResponseType(400)]
        public IActionResult GetResumeById(int resumeId)
        {
            if (!_resumeRepo.ResumeExists(resumeId))
            {
                return NotFound(ModelState);
            }

            var resume = _resumeRepo.GetResumeById(resumeId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(resume);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(402)]
        public IActionResult CreateResume([FromBody] ResumeCreateDto resumeCreateDto)
        {
            if (resumeCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            var resume = new Resume()
            {
                Name = resumeCreateDto.Name,
                FilePath = resumeCreateDto.FilePath,
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_resumeRepo.CreateResume(resume))
            {
                return StatusCode(500, "Something went wrong while trying to save the resume.");
            }

            return StatusCode(201, "Resume saved successfully");
        }

        [HttpPut("{resumeId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateResume(int resumeId, [FromBody] ResumeUpdateDto updatedResume)
        {
            if (updatedResume == null)
                return BadRequest(new {message = "Invalid resume data!"});

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingResume = _resumeRepo.GetResumeById(resumeId);

            if (existingResume == null)
                return NotFound(new {message = $"The resume with resumeId = {resumeId} was not found."});

            _mapper.Map(updatedResume, existingResume);


            if (!_resumeRepo.UpdateResume(existingResume))
            {
                return StatusCode(500, new { message = "Something went wrong while updating the resume, please try again." });
            }

            return NoContent();
        }

    }
}

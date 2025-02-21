using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController: Controller
    {
        private readonly IApplicationRepo _applicationRepo;
        private readonly IStudentRepo _studentRepo;
        private readonly IOpeningRepo _openingRepo;
        private readonly IResumeRepo _resumeRepo;
        private readonly IMapper _mapper;

        public ApplicationController(IStudentRepo studentRepo, IOpeningRepo openingRepo, IResumeRepo resumeRepo, IApplicationRepo applicationRepo, IMapper mapper)
        {
            _applicationRepo = applicationRepo;
            _studentRepo = studentRepo;
            _openingRepo = openingRepo;
            _resumeRepo = resumeRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Application>))]
        [ProducesResponseType(400)]
        public IActionResult GetApplications()
        {
            var applications = _mapper.Map<List<ApplicationDto>>(_applicationRepo.GetApplications());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(applications);
        }


        [HttpGet("{applicationId}")]
        [ProducesResponseType(200, Type = typeof(Application))]
        [ProducesResponseType(400)]
        public IActionResult GetApplicationById(int applicationId)
        {
            if (!_applicationRepo.ApplicationExists(applicationId))
            {
                return NotFound(ModelState);
            }

            var application = _mapper.Map<ApplicationDto>(_applicationRepo.GetApplicationById(applicationId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(application);
        }


        [HttpGet("{applicationId}/opening")]
        [ProducesResponseType(200, Type = typeof(Opening))]
        [ProducesResponseType(400)]
        public IActionResult GetApplicationOpening(int applicationId)
        {
            if (!_applicationRepo.ApplicationExists(applicationId))
            {
                return NotFound(ModelState);
            }

            var opening = _mapper.Map<OpeningDto>(_applicationRepo.GetApplicationOpening(applicationId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(opening);
        }


        [HttpGet("{applicationId}/student")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentByApplicationId(int applicationId)
        {
            if (!_applicationRepo.ApplicationExists(applicationId))
            {
                return NotFound(ModelState);
            }

            var student = _mapper.Map<StudentDto>(_applicationRepo.GetStudentByApplication(applicationId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(student);
        }

        [HttpGet("{applicationId}/resume")]
        [ProducesResponseType(200, Type = typeof(Resume))]
        [ProducesResponseType(400)]
        public IActionResult GetApplicationResume(int applicationId)
        {
            if (!_applicationRepo.ApplicationExists(applicationId))
            {
                return NotFound(ModelState);
            }

            var resume = _applicationRepo.GetApplicationResume(applicationId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(resume);
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(402)]
        public IActionResult CreateApplication([FromBody] ApplicationCreateDto applicationCreateDto)
        {
            if (applicationCreateDto == null)
            {
                return BadRequest(ModelState);
            }
            if (!_studentRepo.StudentExists(applicationCreateDto.StudentId)){
                ModelState.AddModelError("", $"A student, with studentId = {applicationCreateDto.StudentId}, does not exist.");
                return BadRequest(ModelState);
            }

            if (!_openingRepo.OpeningExists(applicationCreateDto.OpeningId))
            {
                ModelState.AddModelError("", $"A opening, with openingId = {applicationCreateDto.OpeningId}, does not exists.");
                return BadRequest(ModelState);
            }
            if (!_resumeRepo.ResumeExists(applicationCreateDto.ResumeId))
            {
                ModelState.AddModelError("", $"The given resume, with resumeId = {applicationCreateDto.ResumeId}, does not exists.");
                return BadRequest(ModelState);
            }

            var application = new Application
            {
                StudentId = applicationCreateDto.StudentId,
                OpeningId = applicationCreateDto.OpeningId,
                ResumeId = applicationCreateDto.ResumeId,
                Status = ApplicationStatus.Pending
            };

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_applicationRepo.CreateApplication(application))
            {
                return StatusCode(500,"Something went wrong while saving the application.");
            }

            return StatusCode(201, "Application saved successfully.");
        }
    }
}

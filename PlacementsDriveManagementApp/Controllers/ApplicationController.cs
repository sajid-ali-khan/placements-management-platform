using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IMapper _mapper;

        public ApplicationController(IApplicationRepo applicationRepo, IMapper mapper)
        {
            _applicationRepo = applicationRepo;
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

    }
}

﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.DTOs;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;
using static System.Net.Mime.MediaTypeNames;
using Application = PlacementsDriveManagementApp.Models.Application;

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


        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ApplicationDetailDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetDetailedApplications()
        {
            var applications = _applicationRepo.GetApplications();
            var detailedApplications = _mapper.Map<List<ApplicationDetailDto>>(applications);

            return Ok(detailedApplications);
        }

        [HttpGet("{applicationId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ApplicationDetailDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetDetailedApplicationById(int applicationId)
        {
            var application = _applicationRepo.GetApplicationById(applicationId);
            var detailedApplication = _mapper.Map<ApplicationDetailDto>(application);

            return Ok(detailedApplication);
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

            string studentId = _studentRepo.GetStudentIdByEmail(applicationCreateDto.StudentEmail);
            if (studentId is null){
                ModelState.AddModelError("", $"A student, with email = {applicationCreateDto.StudentEmail}, does not exist.");
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

            var application = new Application()
            {
                StudentId = studentId,
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

        

        [HttpPut("{applicationId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateApplication(int applicationId, [FromBody] ApplicationUpdateDto application)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingApplication = _applicationRepo.GetApplicationById(applicationId);

            if (existingApplication == null)
                return NotFound();

            existingApplication.Status = application.Status ?? existingApplication.Status;
            existingApplication.InterviewSlot = application.InterviewSlot ?? existingApplication.InterviewSlot;
            existingApplication.StudentAppeared = application.StudentAppeared ?? existingApplication.StudentAppeared;
            existingApplication.Package = application.Package ?? existingApplication.Package;
            existingApplication.JoiningDate = application.JoiningDate ?? existingApplication.JoiningDate;
            existingApplication.PlaceOfWork = application.PlaceOfWork ?? existingApplication.PlaceOfWork;

            if (!_applicationRepo.UpdateApplication(existingApplication))
            {
                ModelState.AddModelError("", "Something went wrong, try again.");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _applicationRepo.Dispose();
                _openingRepo.Dispose();
                _resumeRepo.Dispose();
                _studentRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

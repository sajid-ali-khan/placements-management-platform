using Microsoft.AspNetCore.Mvc;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.Helper;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;
using PlacementsDriveManagementApp.Repository;

namespace PlacementsDriveManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlacementOfficerController: Controller
    {
        private readonly IPlacementOfficerRepo _placementOfficerRepo;
        private readonly PasswordService _passwordService;

        public PlacementOfficerController(IPlacementOfficerRepo placementOfficerRepo, PasswordService passwordService)
        {
            _placementOfficerRepo = placementOfficerRepo;
            _passwordService = passwordService;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<PlacementOfficer>))]
        public IActionResult GetPlacementOfficers()
        {
            var placementOfficers = _placementOfficerRepo.GetPlacementOfficers();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(placementOfficers);
        }

        [HttpGet("{placementOfficerId}")]
        [ProducesResponseType(200, Type = typeof(PlacementOfficer))]
        [ProducesResponseType(400)]
        public IActionResult GetPlacementOfficerById(int placementOfficerId)
        {
            if (!_placementOfficerRepo.PlacementOfficerExists(placementOfficerId))
            {
                return NotFound(ModelState);
            }

            var placementOfficer = _placementOfficerRepo.GetPlacementOfficerById(placementOfficerId);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(placementOfficer);
        }

        [HttpGet("email/{placementOfficerEmail}")]
        [ProducesResponseType(200, Type = typeof(PlacementOfficer))]
        [ProducesResponseType(400)]
        public IActionResult GetPlacementOfficerByEmail(string placementOfficerEmail)
        {
            if (!_placementOfficerRepo.PlacementOfficerExistsByEmail(placementOfficerEmail))
            {
                return NotFound(ModelState);
            }

            var placementOfficer = _placementOfficerRepo.GetPlacementOfficerByEmail(placementOfficerEmail);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(placementOfficer);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult CreatePlacementOfficer([FromBody] PlacementOfficerCreateDto placementOfficerCreateDto)
        {
            if (placementOfficerCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_placementOfficerRepo.PlacementOfficerExistsByEmail(placementOfficerCreateDto.Email))
            {
                ModelState.AddModelError("", $"A placement officer with Email {placementOfficerCreateDto.Email} already exists.");
                return BadRequest(ModelState);
            }

            var hashedPassword = _passwordService.HashPassword(placementOfficerCreateDto.Password);

            var placementOfficer = new PlacementOfficer()
            {
                UserName = placementOfficerCreateDto.UserName,
                Email = placementOfficerCreateDto.Email,
                PasswordHash = hashedPassword,
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_placementOfficerRepo.CreatePlacementOfficer(placementOfficer))
            {
                ModelState.AddModelError("", "Something went wrong while creating the placement officer");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Placement Officer successfully created");

        }

    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OpeningController: Controller
    {
        private readonly IOpeningRepo _openingRepo;
        private readonly IMapper _mapper;

        public OpeningController(IOpeningRepo openingRepo, IMapper mapper)
        {
            _openingRepo = openingRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opening>))]
        public IActionResult GetOpenings()
        {
            var openings = _mapper.Map<List<OpeningDetailDto>>(_openingRepo.GetOpenings());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(openings);
        }

        [HttpGet("{openingId}")]
        [ProducesResponseType(200, Type = typeof(Opening))]
        [ProducesResponseType(400)]
        public IActionResult GetOpeningById(int openingId)
        {
            if (!_openingRepo.OpeningExists(openingId))
            {
                return NotFound(ModelState);
            }

            var opening = _mapper.Map<OpeningDetailDto>(_openingRepo.GetOpeningById(openingId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(opening);
        }

        [HttpGet("{openingId}/company")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyByOpening(int openingId)
        {
            if (!_openingRepo.OpeningExists(openingId))
            {
                return NotFound(ModelState);
            }

            var company = _mapper.Map<CompanyDto>(_openingRepo.GetCompanyByOpening(openingId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(company);
        }

        [HttpGet("{openingId}/applications")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Application>))]
        [ProducesResponseType(400)]
        public IActionResult GetApplicationsByOpening(int openingId)
        {
            if (!_openingRepo.OpeningExists(openingId))
            {
                return NotFound(ModelState);
            }

            var applications = _mapper.Map<List<ApplicationDto>>(_openingRepo.GetApplicationsByOpening(openingId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(applications);
        }


        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(402)]
        public IActionResult CreateOpening([FromBody] OpeningCreateDto openingCreateDto)
        {
            if (openingCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            var openingCheck = _openingRepo.GetOpenings()
                .Where(op => op.CompanyId == openingCreateDto.CompanyId && op.JobTitle == openingCreateDto.JobTitle)
                .FirstOrDefault();

            if (openingCheck != null)
            {
                ModelState.AddModelError("", $"A opening with job title {openingCreateDto.JobTitle} already exists in your company.");
                return BadRequest(ModelState);
            }

            var opening = new Opening()
            {
                CompanyId = openingCreateDto.CompanyId,
                JobTitle = openingCreateDto.JobTitle,
                Description = openingCreateDto.Description,
                LastDate = openingCreateDto.LastDate,
                IsActive = openingCreateDto.IsActive,
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_openingRepo.CreateOpening(opening))
            {
                return StatusCode(500, "Something went wrong while trying to create the opening.");
            }

            return StatusCode(201, "Opening successfully created.");
        }


        [HttpPut("{openingId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateOpening(int openingId, [FromBody] OpeningUpdateDto updatedOpening)
        {
            if (updatedOpening == null)
                return BadRequest(new { message = "Invalid opening data." });

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingOpening = _openingRepo.GetOpeningById(openingId);

            if (existingOpening == null)
                return NotFound(new { message = "Opening not found" });

            _mapper.Map(updatedOpening, existingOpening);

            if (!_openingRepo.UpdateOpening(existingOpening))
            {
                return StatusCode(500, new { message = "Something went wrong." });
            }

            return NoContent();
        }

        [HttpPut("{openingId}/deactivate")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult DeactivateOpening(int openingId)
        {
            var existingOpening = _openingRepo.GetOpeningById(openingId);

            if (existingOpening == null)
                return NotFound(new { message = "Opening not found" });

            existingOpening.IsActive = false;

            if (!_openingRepo.UpdateOpening(existingOpening))
            {
                return StatusCode(500, new { message = "Something went wrong." });
            }

            return NoContent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _openingRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

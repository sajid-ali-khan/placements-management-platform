using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.DTOs;
using PlacementsDriveManagementApp.Helper;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController: Controller
    {
        private readonly ICompanyRepo _companyRepo;
        private readonly PasswordService _passwordService;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepo companyRepo, PasswordService passwordService, IMapper mapper)
        {
            _companyRepo = companyRepo;
            _passwordService = passwordService;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Company>))]
        public IActionResult GetCompanies()
        {
            var companies =  _mapper.Map<List<CompanyDto>>(_companyRepo.GetCompanies());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(companies);
        }


        [HttpGet("{companyId}/openings")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Opening>))]
        [ProducesResponseType(400)]
        public IActionResult GetOpeningsByCompany(int companyId)
        {
            if (!_companyRepo.CompanyExists(companyId))
            {
                return NotFound(ModelState);
            }

            var openings = _mapper.Map<List<OpeningDetailDto>>(_companyRepo.GetCompanyOpenings(companyId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(openings);
        }


        [HttpGet("{companyId}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyById(int companyId)
        {
            if (!_companyRepo.CompanyExists(companyId))
            {
                return NotFound(ModelState);
            }

            var company = _mapper.Map<CompanyDto>(_companyRepo.GetCompanyById(companyId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(company);
        }

        [HttpGet("email/{companyEmail}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyByEmail(string companyEmail)
        {
            if (!_companyRepo.CompanyExistsByEmail(companyEmail))
            {
                return NotFound(ModelState);
            }
            var company = _mapper.Map<CompanyDto>(_companyRepo.GetCompanyByEmail(companyEmail));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(company);
        }


        [HttpGet("email/{companyEmail}/applications")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ApplicationDetailDto>))]
        [ProducesResponseType(400)]
        public IActionResult GetApplicationsByCompany(string companyEmail)
        {
            if (!_companyRepo.CompanyExistsByEmail(companyEmail))
            {
                return NotFound(ModelState);
            }

            var applications = _mapper.Map<List<ApplicationDetailDto>>(_companyRepo.GetApplicationByCompanyEmail(companyEmail));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(applications);
        }

        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public IActionResult CreateCompany([FromBody] CompanyCreateDto companyCreateDto)
        {
            if (companyCreateDto == null)
            {
                return BadRequest(ModelState);
            }

            var compnayCheck = _companyRepo.GetCompanyByEmail(companyCreateDto.Email);

            if (compnayCheck != null)
            {
                ModelState.AddModelError("", $"A company with Email {companyCreateDto.Email} already exists.");
                return BadRequest(ModelState);
            }

            var hashedPassword = _passwordService.HashPassword(companyCreateDto.Password);

            var company = new Company()
            {
                Name = companyCreateDto.Name,
                Email = companyCreateDto.Email,
                PasswordHash = hashedPassword,
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_companyRepo.CreateCompany(company))
            {
                ModelState.AddModelError("", "Something went wrong while creating the company");
                return StatusCode(500, ModelState);
            }

            return StatusCode(201, "Company successfully created");

        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _companyRepo.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

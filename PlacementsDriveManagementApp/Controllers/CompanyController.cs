using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController: Controller
    {
        private readonly ICompanyRepo _companyRepo;
        private readonly IMapper _mapper;

        public CompanyController(ICompanyRepo companyRepo, IMapper mapper)
        {
            _companyRepo = companyRepo;
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
        public IActionResult GetOpeningsByCompany(string companyId)
        {
            if (!_companyRepo.CompanyExists(companyId))
            {
                return NotFound(ModelState);
            }

            var openings = _mapper.Map<List<OpeningDto>>(_companyRepo.GetCompanyOpenings(companyId));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(openings);
        }

        [HttpGet("{companyId}")]
        [ProducesResponseType(200, Type = typeof(Company))]
        [ProducesResponseType(400)]
        public IActionResult GetCompanyById(string companyId)
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
    }
}

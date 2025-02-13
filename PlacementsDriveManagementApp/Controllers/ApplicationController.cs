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
 
    }
}

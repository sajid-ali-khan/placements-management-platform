
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PlacementsDriveManagementApp.Dto;
using PlacementsDriveManagementApp.Interfaces;
using PlacementsDriveManagementApp.Models;

namespace PlacementsDriveManagementApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepo studentRepo,IMapper mapper)
        {
            _studentRepo = studentRepo;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Student>) )]
        [ProducesResponseType(400)]
        public IActionResult GetStudents()
        {
            var students = _mapper.Map<List<StudentDto>>(_studentRepo.GetStudents());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(students);
        }
        [HttpGet("{studentId}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentById(string studentId)
        {
            var students = _mapper.Map<StudentDto>(_studentRepo.GetStudentById(studentId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(students);
        }
        [HttpGet("{studentId}/applications")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Application>))]
        [ProducesResponseType(400)]
        public IActionResult GetApplicationsByStudent(string studentId)
        {
            var students = _mapper.Map<List<ApplicationDto>>(_studentRepo.GetApplicationsByStudent(studentId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(students);
        }



    }
}

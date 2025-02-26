
using AutoMapper;
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
    public class StudentController : Controller
    {
        private readonly IStudentRepo _studentRepo;
        private readonly PasswordService _passwordService;
        private readonly IMapper _mapper;

        public StudentController(IStudentRepo studentRepo, PasswordService passwordService,IMapper mapper)
        {
            _studentRepo = studentRepo;
            _passwordService = passwordService;
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
            if (!_studentRepo.StudentExists(studentId))
            {
                return NotFound(ModelState);
            }
            var students = _mapper.Map<StudentDto>(_studentRepo.GetStudentById(studentId));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(students);
        }

        [HttpGet("email/{studentEmail}")]
        [ProducesResponseType(200, Type = typeof(Student))]
        [ProducesResponseType(400)]
        public IActionResult GetStudentsByEmail(string studentEmail)
        {
            if (!_studentRepo.StudentExistsByEmail(studentEmail))
            {
                return NotFound(ModelState);
            }
            var students = _mapper.Map<StudentDto>(_studentRepo.GetStudentByEmail(studentEmail));
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



        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(402)]
        public IActionResult CreateStudent([FromBody] StudentCreateDto studentDto)
        {
            if (studentDto == null)
            {
                return BadRequest(ModelState);
            }

            if (_studentRepo.StudentExists(studentDto.Id))
            {
                ModelState.AddModelError("", $"A student with Id = {studentDto.Id} already exists.");
                return BadRequest(ModelState);
            }

            if (_studentRepo.StudentExistsByEmail(studentDto.Email))
            {
                ModelState.AddModelError("", $"A student with Email = {studentDto.Email} already exists.");
            }

            var hashedPassword = _passwordService.HashPassword(studentDto.Password);

            var student = new Student()
            {
                Id = studentDto.Id,
                Name = studentDto.Name,
                Phone = studentDto.Phone,
                Email = studentDto.Email,
                Dob = studentDto.Dob,
                PassWord = hashedPassword,
            };

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!_studentRepo.CreateStudent(student))
            {
                return StatusCode(500, "Something went wrong while trying to add the student.");
            }

            return StatusCode(201, "Student saved successfully");
        }


        [HttpPut("{studentId}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [ProducesResponseType(400)]
        public IActionResult UpdateStudent(string studentId, [FromBody] StudentUpdateDto updatedStudent)
        {
            if (updatedStudent == null)
                return BadRequest(ModelState);


            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingStudent = _studentRepo.GetStudentById(studentId);

            if (existingStudent == null)
                return NotFound();

            var check = _studentRepo.GetStudents()
                .Any(s => s.Email.ToUpper() == updatedStudent.Email.ToUpper() && s.Id.ToUpper() != studentId.ToUpper());

            if (check)
            {
                ModelState.AddModelError("", $"The email, {updatedStudent.Email} is assigned to someone else.");
                return BadRequest(ModelState);
            }

            _mapper.Map(updatedStudent, existingStudent);

            if (!_studentRepo.UpdateStudent(existingStudent))
            {
                ModelState.AddModelError("", "Something went wrong while trying to update the student");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }
    }
}

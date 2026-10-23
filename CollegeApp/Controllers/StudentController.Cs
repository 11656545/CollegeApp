using Microsoft.AspNetCore.Mvc;
using CollegeApp.Models;
namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        [HttpGet("All")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            var students = CollegeRepository.Students.Select(s => new StudentDTO
            {
                Id = s.Id,
                Name = s.Name,
                Email = s.Email,
                Address = s.Address
            }).ToList();

            return Ok(students);
        }

        [HttpGet("{id}:int", Name = "GetStudentById")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID was entered");
            }
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with {id} not found");
            }
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Address = student.Address
            };
            return Ok(studentDTO);
        }
        [HttpGet]
        [Route("{name}:alpha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> GetStudentByName(string name)
        {
            var student = CollegeRepository.Students.Where(n => n.Name == name).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with name {name} not found");
            }
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                Name = student.Name,
                Email = student.Email,
                Address = student.Address
            };
            return Ok(studentDTO);
        }

        [HttpPost("Create")]
        public ActionResult<StudentDTO> CreateStudent([FromBody] Student model)
        {
            if (model == null)
            {
                return BadRequest("Student data is null");
            }
            int newId = CollegeRepository.Students.Max(s => s.Id) + 1;
            Student student = new Student
            {
                Id = newId,
                Name = model.Name,
                Email = model.Email,
                Address = model.Address
            };
            CollegeRepository.Students.Add(student);
            model.Id = student.Id;
            return CreatedAtRoute("GetStudentById", new { id = student.Id }, model);
        }

        [HttpDelete("{id}:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<bool> DeleteStudentByID(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid student ID was entered");
            }
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with {id} not found");
            }
            CollegeRepository.Students.Remove(student);
            return Ok(true);
        }
        
    }
}

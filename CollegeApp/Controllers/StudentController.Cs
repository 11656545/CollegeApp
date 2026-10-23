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
        public IEnumerable<Student> GetStudents()
        {
            return CollegeRepository.Students;
        }

        [HttpGet("{id}:int")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentById(int id)
        {
            if(id <= 0)
            {
                return BadRequest("Invalid student ID was entered");
            }
            var student = CollegeRepository.Students.Where(n => n.Id == id).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with {id} not found");
            }
            return Ok(student);
        }
        [HttpGet]
        [Route("{name}:alpha")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Student> GetStudentByName(string name)
        {
            var student = CollegeRepository.Students.Where(n => n.Name == name).FirstOrDefault();
            if (student == null)
            {
                return NotFound($"Student with name {name} not found");
            }
            return Ok(student);
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
            if(student == null)
            {
                return NotFound($"Student with {id} not found");
            }
            CollegeRepository.Students.Remove(student);
            return Ok(true);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using CollegeApp.Models;
using Microsoft.AspNetCore.JsonPatch;

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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
        {
            //Manual validation
            //if (!ModelState.IsValid)
            //    return BadRequest(ModelState);
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

        [HttpPut("{id:int}")]
        public ActionResult<StudentDTO> UpdateStudent([FromBody] StudentDTO model)
        {
            if (model == null)
            {
                return BadRequest("Student data is null");
            }
            var existingstudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();
            if (existingstudent == null)
            {
                return NotFound($"Student with ID {model.Id} not found");
            }
            if (string.IsNullOrEmpty(model.Name) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Address))
            {
                return BadRequest("Student data is incomplete");
            }
            existingstudent.Name = model.Name;
            existingstudent.Email = model.Email;
            existingstudent.Address = model.Address;
            return Ok(model);
        }

        [HttpPatch]
        [Route("{id:int}/UpdatePartial")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateStudentPartial(int id, [FromBody] JsonPatchDocument<StudentDTO> patchmodel)
        {
            if(patchmodel == null)
            {
                return BadRequest("Student data is null");
            }
            if (id <= 0)
            {
                return BadRequest("Invalid student ID was entered");
            }
            var existingstudent = CollegeRepository.Students.Where(s => s.Id == id).FirstOrDefault();
            var studentDTO = new StudentDTO
            {
                Id = existingstudent.Id,
                Name = existingstudent.Name,
                Email = existingstudent.Email,
                Address = existingstudent.Address
            };

            patchmodel.ApplyTo(studentDTO);

            if(string.IsNullOrEmpty(studentDTO.Name) || string.IsNullOrEmpty(studentDTO.Email) || string.IsNullOrEmpty(studentDTO.Address))
            {
                return BadRequest("Student data is incomplete");
            }
            existingstudent.Name = studentDTO.Name;
            existingstudent.Email = studentDTO.Email;
            existingstudent.Address = studentDTO.Address;

            return NoContent();
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
